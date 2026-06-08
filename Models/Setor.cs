using System.ComponentModel.DataAnnotations;

namespace SistemaMedicacoes.Models;

public class Setor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(45)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    public string Andar { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Descricao { get; set; } = string.Empty;

    public List<Paciente> Pacientes { get; set; } = new();
}
