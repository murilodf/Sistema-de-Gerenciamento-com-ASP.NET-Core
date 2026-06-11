using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Data;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public SetoresController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todos os setores cadastrados.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetSetores()
    {
        var setores = await _context.Setores
            .AsNoTracking()
            .Select(s => new
            {
                s.Id,
                s.Nome,
                s.Andar,
                s.Descricao,
                TotalPacientes = s.Pacientes.Count
            })
            .ToListAsync();

        return Ok(setores);
    }

    /// <summary>Busca um setor pelo id, incluindo seus pacientes.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetSetor(int id)
    {
        var setor = await _context.Setores
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new
            {
                s.Id,
                s.Nome,
                s.Andar,
                s.Descricao,
                Pacientes = s.Pacientes.Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Leito
                })
            })
            .FirstOrDefaultAsync();

        return setor is null ? NotFound("Setor nao encontrado.") : Ok(setor);
    }

    /// <summary>Cadastra um novo setor hospitalar.</summary>
    [HttpPost]
    public async Task<ActionResult<Setor>> PostSetor(Setor setor)
    {
        _context.Setores.Add(setor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSetor), new { id = setor.Id }, setor);
    }

    /// <summary>Atualiza os dados de um setor existente.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSetor(int id, Setor setor)
    {
        if (id != setor.Id)
        {
            return BadRequest("O id da rota deve ser igual ao id do setor.");
        }

        if (!await _context.Setores.AnyAsync(s => s.Id == id))
        {
            return NotFound("Setor nao encontrado.");
        }

        _context.Entry(setor).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Exclui um setor, desde que ele nao possua pacientes vinculados.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSetor(int id)
    {
        var setor = await _context.Setores.FindAsync(id);
        if (setor is null)
        {
            return NotFound("Setor nao encontrado.");
        }

        var possuiPacientes = await _context.Pacientes.AnyAsync(p => p.SetorId == id);
        if (possuiPacientes)
        {
            return Conflict("Nao e possivel excluir um setor com pacientes cadastrados.");
        }

        _context.Setores.Remove(setor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
