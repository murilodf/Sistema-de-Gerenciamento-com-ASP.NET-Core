using System.ComponentModel.DataAnnotations;

namespace SistemaMedicacoes.Models;

public class PrescricaoMedicamento
{
    public int Id { get; set; }

    public int Quantidade { get; set; }

    [Required]
    [MaxLength(45)]
    public string Frequencia { get; set; } = string.Empty;

    public TimeSpan Horario { get; set; }

    public int PrescricaoId { get; set; }
    public Prescricao? Prescricao { get; set; }

    public int MedicamentoId { get; set; }
    public Medicamento? Medicamento { get; set; }
}
