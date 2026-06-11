using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescricaoMedicamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PrescricaoMedicamentosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todos os medicamentos vinculados a prescricoes.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetPrescricaoMedicamentos()
    {
        var itens = await _context.PrescricaoMedicamentos
            .AsNoTracking()
            .Select(pm => new
            {
                pm.Id,
                pm.PrescricaoId,
                pm.MedicamentoId,
                Medicamento = pm.Medicamento == null ? null : pm.Medicamento.Nome,
                pm.Quantidade,
                pm.Frequencia,
                pm.Horario
            })
            .ToListAsync();

        return Ok(itens);
    }

    /// <summary>Busca um item de prescricao pelo id.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetPrescricaoMedicamento(int id)
    {
        var item = await _context.PrescricaoMedicamentos
            .AsNoTracking()
            .Where(pm => pm.Id == id)
            .Select(pm => new
            {
                pm.Id,
                pm.PrescricaoId,
                pm.MedicamentoId,
                Medicamento = pm.Medicamento == null ? null : pm.Medicamento.Nome,
                pm.Quantidade,
                pm.Frequencia,
                pm.Horario
            })
            .FirstOrDefaultAsync();

        return item is null ? NotFound("Item da prescricao nao encontrado.") : Ok(item);
    }

    /// <summary>Vincula um medicamento a uma prescricao existente.</summary>
    [HttpPost]
    public async Task<ActionResult<PrescricaoMedicamento>> PostPrescricaoMedicamento(PrescricaoMedicamento item)
    {
        var erro = await ValidarItemPrescricao(item);
        if (erro is not null)
        {
            return BadRequest(erro);
        }

        _context.PrescricaoMedicamentos.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrescricaoMedicamento), new { id = item.Id }, item);
    }

    /// <summary>Atualiza quantidade, frequencia ou horario de um medicamento prescrito.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPrescricaoMedicamento(int id, PrescricaoMedicamento item)
    {
        if (id != item.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id do item da prescricao.");
        }

        if (!await _context.PrescricaoMedicamentos.AnyAsync(pm => pm.Id == id))
        {
            return NotFound("Item da prescricao nao encontrado.");
        }

        var erro = await ValidarItemPrescricao(item);
        if (erro is not null)
        {
            return BadRequest(erro);
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Remove um medicamento de uma prescricao.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrescricaoMedicamento(int id)
    {
        var item = await _context.PrescricaoMedicamentos.FindAsync(id);
        if (item is null)
        {
            return NotFound("Item da prescricao nao encontrado.");
        }

        _context.PrescricaoMedicamentos.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<string?> ValidarItemPrescricao(PrescricaoMedicamento item)
    {
        if (item.Quantidade <= 0)
        {
            return "A quantidade deve ser maior que zero.";
        }

        if (!await _context.Prescricoes.AnyAsync(p => p.Id == item.PrescricaoId))
        {
            return "Prescricao informada nao existe.";
        }

        if (!await _context.Medicamentos.AnyAsync(m => m.Id == item.MedicamentoId))
        {
            return "Medicamento informado nao existe.";
        }

        return null;
    }
}
