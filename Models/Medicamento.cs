using System.ComponentModel.DataAnnotations;

namespace SistemaMedicacoes.Models;

public class Medicamento
{
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    public string Dosagem { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    public string ViaAdministracao { get; set; } = string.Empty;

    public int Estoque { get; set; }

    public List<PrescricaoMedicamento> PrescricaoMedicamentos { get; set; } = new();
}
