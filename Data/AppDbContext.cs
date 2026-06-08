using Microsoft.EntityFrameworkCore;
using SistemaMedicacoes.Models;

namespace SistemaMedicacoes.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Setor> Setores => Set<Setor>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Prescricao> Prescricoes => Set<Prescricao>();
    public DbSet<PrescricaoMedicamento> PrescricaoMedicamentos => Set<PrescricaoMedicamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Setor>().ToTable("SETOR");
        modelBuilder.Entity<Paciente>().ToTable("PACIENTE");
        modelBuilder.Entity<Medico>().ToTable("MEDICO");
        modelBuilder.Entity<Medicamento>().ToTable("MEDICAMENTO");
        modelBuilder.Entity<Prescricao>().ToTable("PRESCRICAO");
        modelBuilder.Entity<PrescricaoMedicamento>().ToTable("PRESCRICAO_MEDICAMENTO");

        modelBuilder.Entity<Setor>()
            .HasMany(s => s.Pacientes)
            .WithOne(p => p.Setor)
            .HasForeignKey(p => p.SetorId);

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Prescricoes)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteId);

        modelBuilder.Entity<Medico>()
            .HasMany(m => m.Prescricoes)
            .WithOne(p => p.Medico)
            .HasForeignKey(p => p.MedicoId);

        modelBuilder.Entity<PrescricaoMedicamento>()
            .HasOne(pm => pm.Prescricao)
            .WithMany(p => p.PrescricaoMedicamentos)
            .HasForeignKey(pm => pm.PrescricaoId);

        modelBuilder.Entity<PrescricaoMedicamento>()
            .HasOne(pm => pm.Medicamento)
            .WithMany(m => m.PrescricaoMedicamentos)
            .HasForeignKey(pm => pm.MedicamentoId);

        modelBuilder.Entity<Setor>().HasData(
            new Setor { Id = 1, Nome = "UTI", Andar = "1", Descricao = "Unidade de terapia intensiva" },
            new Setor { Id = 2, Nome = "Enfermaria", Andar = "2", Descricao = "Setor de internacao" }
        );

        modelBuilder.Entity<Medico>().HasData(
            new Medico { Id = 1, Nome = "Joao Pereira", Crm = "123456", Especialidade = "Clinico Geral", Telefone = "14999990000" },
            new Medico { Id = 2, Nome = "Ana Souza", Crm = "654321", Especialidade = "Cardiologia", Telefone = "14999991111" }
        );

        modelBuilder.Entity<Paciente>().HasData(
            new Paciente { Id = 1, Nome = "Maria Oliveira", Cpf = "11122233344", DataNascimento = new DateTime(1985, 5, 10), Leito = "A101", Telefone = "14988880000", SetorId = 1 },
            new Paciente { Id = 2, Nome = "Carlos Santos", Cpf = "55566677788", DataNascimento = new DateTime(1978, 8, 20), Leito = "B202", Telefone = "14988881111", SetorId = 2 }
        );

        modelBuilder.Entity<Medicamento>().HasData(
            new Medicamento { Id = 1, Nome = "Dipirona", Dosagem = "500mg", ViaAdministracao = "Oral", Estoque = 100 },
            new Medicamento { Id = 2, Nome = "Amoxicilina", Dosagem = "875mg", ViaAdministracao = "Oral", Estoque = 50 }
        );

        modelBuilder.Entity<Prescricao>().HasData(
            new Prescricao { Id = 1, DataPrescricao = new DateTime(2026, 5, 26), Observacao = "Paciente em observacao", MedicoId = 1, PacienteId = 1 }
        );

        modelBuilder.Entity<PrescricaoMedicamento>().HasData(
            new PrescricaoMedicamento { Id = 1, PrescricaoId = 1, MedicamentoId = 1, Quantidade = 1, Frequencia = "8 em 8 horas", Horario = new TimeSpan(8, 0, 0) },
            new PrescricaoMedicamento { Id = 2, PrescricaoId = 1, MedicamentoId = 2, Quantidade = 1, Frequencia = "12 em 12 horas", Horario = new TimeSpan(10, 0, 0) }
        );
    }
}
