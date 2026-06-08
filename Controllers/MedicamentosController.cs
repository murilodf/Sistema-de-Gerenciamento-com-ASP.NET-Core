using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicamentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetMedicamentos()
    {
        var medicamentos = await _context.Medicamentos
            .AsNoTracking()
            .Select(m => new
            {
                m.Id,
                m.Nome,
                m.Dosagem,
                m.ViaAdministracao,
                m.Estoque
            })
            .ToListAsync();

        return Ok(medicamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetMedicamento(int id)
    {
        var medicamento = await _context.Medicamentos
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Id,
                m.Nome,
                m.Dosagem,
                m.ViaAdministracao,
                m.Estoque,
                Prescricoes = m.PrescricaoMedicamentos.Select(pm => new
                {
                    pm.PrescricaoId,
                    pm.Quantidade,
                    pm.Frequencia,
                    pm.Horario
                })
            })
            .FirstOrDefaultAsync();

        return medicamento is null ? NotFound("Medicamento nao encontrado.") : Ok(medicamento);
    }

    [HttpPost]
    public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
    {
        if (medicamento.Estoque < 0)
        {
            return BadRequest("O estoque nao pode ser negativo.");
        }

        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.Id }, medicamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
    {
        if (id != medicamento.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id do medicamento.");
        }

        if (medicamento.Estoque < 0)
        {
            return BadRequest("O estoque nao pode ser negativo.");
        }

        if (!await _context.Medicamentos.AnyAsync(m => m.Id == id))
        {
            return NotFound("Medicamento nao encontrado.");
        }

        _context.Entry(medicamento).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicamento(int id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);
        if (medicamento is null)
        {
            return NotFound("Medicamento nao encontrado.");
        }

        var estaEmPrescricao = await _context.PrescricaoMedicamentos.AnyAsync(pm => pm.MedicamentoId == id);
        if (estaEmPrescricao)
        {
            return Conflict("Nao e possivel excluir um medicamento usado em prescricoes.");
        }

        _context.Medicamentos.Remove(medicamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
