using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PacientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetPacientes()
    {
        var pacientes = await _context.Pacientes
            .AsNoTracking()
            .Select(p => new
            {
                p.Id,
                p.Nome,
                p.Cpf,
                p.DataNascimento,
                p.Leito,
                p.Telefone,
                p.SetorId,
                Setor = p.Setor == null ? null : p.Setor.Nome
            })
            .ToListAsync();

        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetPaciente(int id)
    {
        var paciente = await _context.Pacientes
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Nome,
                p.Cpf,
                p.DataNascimento,
                p.Leito,
                p.Telefone,
                p.SetorId,
                Setor = p.Setor == null ? null : p.Setor.Nome,
                Prescricoes = p.Prescricoes.Select(pr => new
                {
                    pr.Id,
                    pr.DataPrescricao,
                    pr.Observacao,
                    pr.MedicoId
                })
            })
            .FirstOrDefaultAsync();

        return paciente is null ? NotFound("Paciente nao encontrado.") : Ok(paciente);
    }

    [HttpPost]
    public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
    {
        if (!await _context.Setores.AnyAsync(s => s.Id == paciente.SetorId))
        {
            return BadRequest("Setor informado nao existe.");
        }

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
    {
        if (id != paciente.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id do paciente.");
        }

        if (!await _context.Pacientes.AnyAsync(p => p.Id == id))
        {
            return NotFound("Paciente nao encontrado.");
        }

        if (!await _context.Setores.AnyAsync(s => s.Id == paciente.SetorId))
        {
            return BadRequest("Setor informado nao existe.");
        }

        _context.Entry(paciente).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaciente(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente is null)
        {
            return NotFound("Paciente nao encontrado.");
        }

        var possuiPrescricoes = await _context.Prescricoes.AnyAsync(p => p.PacienteId == id);
        if (possuiPrescricoes)
        {
            return Conflict("Nao e possivel excluir um paciente com prescricoes cadastradas.");
        }

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
