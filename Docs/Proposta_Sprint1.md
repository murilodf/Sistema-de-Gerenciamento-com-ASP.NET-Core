# Proposta da Sprint 1

## Nome do sistema

Sistema de Controle de Medicacoes Hospitalares

## Justificativa

O sistema tem como objetivo controlar pacientes, medicos, setores, medicamentos e prescricoes medicas em ambiente hospitalar.

A proposta organiza o cadastro de pacientes e medicamentos, alem de permitir o registro de prescricoes feitas por medicos.

O tema foi escolhido por representar um problema real da area da saude e permitir a criacao de entidades relacionadas, incluindo relacionamento um para muitos e muitos para muitos.

## Entidades

1. Setor
2. Paciente
3. Medico
4. Medicamento
5. Prescricao
6. PrescricaoMedicamento

## Relacionamentos

- Um setor pode ter varios pacientes.
- Um paciente pertence a um setor.
- Um paciente pode ter varias prescricoes.
- Uma prescricao pertence a um paciente.
- Um medico pode fazer varias prescricoes.
- Uma prescricao pertence a um medico.
- Uma prescricao pode ter varios medicamentos.
- Um medicamento pode aparecer em varias prescricoes.

## Relacionamento N:M

O relacionamento muitos para muitos ocorre entre Prescricao e Medicamento.

Esse relacionamento usa a tabela intermediaria PrescricaoMedicamento.

## Itens tecnicos da Sprint 1

- Projeto ASP.NET Core Web API criado.
- Entity Framework Core configurado.
- Banco relacional MySQL configurado.
- Migration inicial criada.
- Seed de dados iniciais configurado.
- Diagrama ER documentado.
