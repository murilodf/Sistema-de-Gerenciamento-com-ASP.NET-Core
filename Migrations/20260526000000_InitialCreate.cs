using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SistemaMedicacoes.Data;

#nullable disable

namespace SistemaMedicacoes.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260526000000_InitialCreate")]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MEDICAMENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dosagem = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ViaAdministracao = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estoque = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICAMENTO", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MEDICO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Crm = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Especialidade = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICO", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Andar = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PACIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Leito = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SetorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PACIENTE_SETOR_SetorId",
                        column: x => x.SetorId,
                        principalTable: "SETOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRESCRICAO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataPrescricao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRESCRICAO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRESCRICAO_MEDICO_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "MEDICO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRESCRICAO_PACIENTE_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "PACIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRESCRICAO_MEDICAMENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Frequencia = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Horario = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    PrescricaoId = table.Column<int>(type: "int", nullable: false),
                    MedicamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRESCRICAO_MEDICAMENTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRESCRICAO_MEDICAMENTO_MEDICAMENTO_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "MEDICAMENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRESCRICAO_MEDICAMENTO_PRESCRICAO_PrescricaoId",
                        column: x => x.PrescricaoId,
                        principalTable: "PRESCRICAO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "MEDICAMENTO",
                columns: new[] { "Id", "Dosagem", "Estoque", "Nome", "ViaAdministracao" },
                values: new object[,]
                {
                    { 1, "500mg", 100, "Dipirona", "Oral" },
                    { 2, "875mg", 50, "Amoxicilina", "Oral" }
                });

            migrationBuilder.InsertData(
                table: "MEDICO",
                columns: new[] { "Id", "Crm", "Especialidade", "Nome", "Telefone" },
                values: new object[,]
                {
                    { 1, "123456", "Clinico Geral", "Joao Pereira", "14999990000" },
                    { 2, "654321", "Cardiologia", "Ana Souza", "14999991111" }
                });

            migrationBuilder.InsertData(
                table: "SETOR",
                columns: new[] { "Id", "Andar", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "1", "Unidade de terapia intensiva", "UTI" },
                    { 2, "2", "Setor de internacao", "Enfermaria" }
                });

            migrationBuilder.InsertData(
                table: "PACIENTE",
                columns: new[] { "Id", "Cpf", "DataNascimento", "Leito", "Nome", "SetorId", "Telefone" },
                values: new object[,]
                {
                    { 1, "11122233344", new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A101", "Maria Oliveira", 1, "14988880000" },
                    { 2, "55566677788", new DateTime(1978, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "B202", "Carlos Santos", 2, "14988881111" }
                });

            migrationBuilder.InsertData(
                table: "PRESCRICAO",
                columns: new[] { "Id", "DataPrescricao", "MedicoId", "Observacao", "PacienteId" },
                values: new object[] { 1, new DateTime(2026, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Paciente em observacao", 1 });

            migrationBuilder.InsertData(
                table: "PRESCRICAO_MEDICAMENTO",
                columns: new[] { "Id", "Frequencia", "Horario", "MedicamentoId", "PrescricaoId", "Quantidade" },
                values: new object[,]
                {
                    { 1, "8 em 8 horas", new TimeSpan(0, 8, 0, 0, 0), 1, 1, 1 },
                    { 2, "12 em 12 horas", new TimeSpan(0, 10, 0, 0, 0), 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTE_SetorId",
                table: "PACIENTE",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_PRESCRICAO_MedicoId",
                table: "PRESCRICAO",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PRESCRICAO_PacienteId",
                table: "PRESCRICAO",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PRESCRICAO_MEDICAMENTO_MedicamentoId",
                table: "PRESCRICAO_MEDICAMENTO",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PRESCRICAO_MEDICAMENTO_PrescricaoId",
                table: "PRESCRICAO_MEDICAMENTO",
                column: "PrescricaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PRESCRICAO_MEDICAMENTO");
            migrationBuilder.DropTable(name: "MEDICAMENTO");
            migrationBuilder.DropTable(name: "PRESCRICAO");
            migrationBuilder.DropTable(name: "MEDICO");
            migrationBuilder.DropTable(name: "PACIENTE");
            migrationBuilder.DropTable(name: "SETOR");
        }
    }
}
