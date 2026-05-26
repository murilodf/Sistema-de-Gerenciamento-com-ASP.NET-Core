using System.ComponentModel.DataAnnotations;

namespace SistemaMedicacoes.Models;

public class Prescricao
{
    public int Id { get; set; }

    public DateTime DataPrescricao { get; set; }

    [MaxLength(100)]
    public string Observacao { get; set; } = string.Empty;

    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }

    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public List<PrescricaoMedicamento> PrescricaoMedicamentos { get; set; } = new();
}
