using System.ComponentModel.DataAnnotations;

namespace SistemaMedicacoes.Models;

public class Medico
{
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Crm { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    public string Especialidade { get; set; } = string.Empty;

    [MaxLength(11)]
    public string Telefone { get; set; } = string.Empty;

    public List<Prescricao> Prescricoes { get; set; } = new();
}
