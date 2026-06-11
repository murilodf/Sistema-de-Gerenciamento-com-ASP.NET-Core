using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescricoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PrescricoesController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todas as prescricoes cadastradas.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetPrescricoes()
    {
        var prescricoes = await _context.Prescricoes
            .AsNoTracking()
            .Select(p => new
            {
                p.Id,
                p.DataPrescricao,
                p.Observacao,
                p.MedicoId,
                Medico = p.Medico == null ? null : p.Medico.Nome,
                p.PacienteId,
                Paciente = p.Paciente == null ? null : p.Paciente.Nome
            })
            .ToListAsync();

        return Ok(prescricoes);
    }

    /// <summary>Busca uma prescricao pelo id, incluindo medico, paciente e medicamentos.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetPrescricao(int id)
    {
        var prescricao = await _context.Prescricoes
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.DataPrescricao,
                p.Observacao,
                p.MedicoId,
                Medico = p.Medico == null ? null : p.Medico.Nome,
                p.PacienteId,
                Paciente = p.Paciente == null ? null : p.Paciente.Nome,
                Medicamentos = p.PrescricaoMedicamentos.Select(pm => new
                {
                    pm.Id,
                    pm.MedicamentoId,
                    Medicamento = pm.Medicamento == null ? null : pm.Medicamento.Nome,
                    pm.Quantidade,
                    pm.Frequencia,
                    pm.Horario
                })
            })
            .FirstOrDefaultAsync();

        return prescricao is null ? NotFound("Prescricao nao encontrada.") : Ok(prescricao);
    }

    /// <summary>Cadastra uma nova prescricao para um paciente e medico existentes.</summary>
    [HttpPost]
    public async Task<ActionResult<Prescricao>> PostPrescricao(Prescricao prescricao)
    {
        if (!await _context.Medicos.AnyAsync(m => m.Id == prescricao.MedicoId))
        {
            return BadRequest("Medico informado nao existe.");
        }

        if (!await _context.Pacientes.AnyAsync(p => p.Id == prescricao.PacienteId))
        {
            return BadRequest("Paciente informado nao existe.");
        }

        if (prescricao.DataPrescricao == default)
        {
            prescricao.DataPrescricao = DateTime.Now;
        }

        _context.Prescricoes.Add(prescricao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrescricao), new { id = prescricao.Id }, prescricao);
    }

    /// <summary>Atualiza os dados de uma prescricao existente.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPrescricao(int id, Prescricao prescricao)
    {
        if (id != prescricao.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id da prescricao.");
        }

        if (!await _context.Prescricoes.AnyAsync(p => p.Id == id))
        {
            return NotFound("Prescricao nao encontrada.");
        }

        if (!await _context.Medicos.AnyAsync(m => m.Id == prescricao.MedicoId))
        {
            return BadRequest("Medico informado nao existe.");
        }

        if (!await _context.Pacientes.AnyAsync(p => p.Id == prescricao.PacienteId))
        {
            return BadRequest("Paciente informado nao existe.");
        }

        _context.Entry(prescricao).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Exclui uma prescricao, desde que ela nao possua medicamentos vinculados.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrescricao(int id)
    {
        var prescricao = await _context.Prescricoes.FindAsync(id);
        if (prescricao is null)
        {
            return NotFound("Prescricao nao encontrada.");
        }

        var possuiMedicamentos = await _context.PrescricaoMedicamentos.AnyAsync(pm => pm.PrescricaoId == id);
        if (possuiMedicamentos)
        {
            return Conflict("Nao e possivel excluir uma prescricao com medicamentos vinculados.");
        }

        _context.Prescricoes.Remove(prescricao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
