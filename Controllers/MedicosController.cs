using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todos os medicos cadastrados.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetMedicos()
    {
        var medicos = await _context.Medicos
            .AsNoTracking()
            .Select(m => new
            {
                m.Id,
                m.Nome,
                m.Crm,
                m.Especialidade,
                m.Telefone,
                TotalPrescricoes = m.Prescricoes.Count
            })
            .ToListAsync();

        return Ok(medicos);
    }

    /// <summary>Busca um medico pelo id, incluindo suas prescricoes.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetMedico(int id)
    {
        var medico = await _context.Medicos
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Id,
                m.Nome,
                m.Crm,
                m.Especialidade,
                m.Telefone,
                Prescricoes = m.Prescricoes.Select(p => new
                {
                    p.Id,
                    p.DataPrescricao,
                    p.PacienteId
                })
            })
            .FirstOrDefaultAsync();

        return medico is null ? NotFound("Medico nao encontrado.") : Ok(medico);
    }

    /// <summary>Cadastra um novo medico.</summary>
    [HttpPost]
    public async Task<ActionResult<Medico>> PostMedico(Medico medico)
    {
        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMedico), new { id = medico.Id }, medico);
    }

    /// <summary>Atualiza os dados de um medico existente.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedico(int id, Medico medico)
    {
        if (id != medico.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id do medico.");
        }

        if (!await _context.Medicos.AnyAsync(m => m.Id == id))
        {
            return NotFound("Medico nao encontrado.");
        }

        _context.Entry(medico).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Exclui um medico, desde que ele nao possua prescricoes vinculadas.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedico(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico is null)
        {
            return NotFound("Medico nao encontrado.");
        }

        var possuiPrescricoes = await _context.Prescricoes.AnyAsync(p => p.MedicoId == id);
        if (possuiPrescricoes)
        {
            return Conflict("Nao e possivel excluir um medico com prescricoes cadastradas.");
        }

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
