# Sistema de Controle de Medicacoes Hospitalares

Projeto da Sprint 1 da disciplina Plataforma de Desenvolvimento de Software.

## Tema

Sistema de Controle de Medicacoes Hospitalares.

## Objetivo

Criar uma API em ASP.NET Core com Entity Framework Core para controlar setores, pacientes, medicos, medicamentos e prescricoes medicas em ambiente hospitalar.

## Entidades

- Setor
- Paciente
- Medico
- Medicamento
- Prescricao
- PrescricaoMedicamento

## Relacionamentos

- Setor 1:N Paciente
- Paciente 1:N Prescricao
- Medico 1:N Prescricao
- Prescricao N:M Medicamento, usando PrescricaoMedicamento como tabela intermediaria

## Tecnologias

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger/OpenAPI

## Entrega da Sprint 1

Itens solicitados no enunciado:

- Proposta de tema
- Justificativa do tema
- Diagrama ER
- Projeto ASP.NET Core criado
- Arquitetura minima com Controllers, Models e Data
- Entity Framework Core configurado
- Migration inicial
- Seed de dados iniciais
- Banco relacional MySQL

Os documentos da Sprint 1 estao na pasta `Docs`:

- `Docs/Proposta_Sprint1.md`
- `Docs/DiagramaER_Mermaid.md`
- `Docs/banco_mysql.sql`

## Como rodar

1. Confirme se o MySQL esta rodando na porta 3306.

2. Confirme a connection string em `appsettings.json`.

```json
"DefaultConnection": "server=localhost;port=3306;database=medcontrol_db;user=medcontrol;password=medcontrol123"
```

3. Restaure os pacotes.

```bash
dotnet restore
```

4. Instale o Entity Framework CLI, se ainda nao tiver.

```bash
dotnet tool install --global dotnet-ef
```

5. Rode a migration.

```bash
dotnet ef database update
```

6. Execute o projeto.

```bash
dotnet run --urls http://localhost:5041
```

7. Abra o Swagger.

```txt
http://localhost:5041/swagger
```

## Observacao

Para a entrega, subir este projeto em um repositorio GitHub e informar o link do repositorio conforme solicitado no enunciado.
