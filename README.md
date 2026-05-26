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
