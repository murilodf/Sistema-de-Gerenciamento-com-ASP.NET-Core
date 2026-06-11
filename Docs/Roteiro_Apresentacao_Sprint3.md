# Roteiro de Apresentacao da Sprint 3

Tempo sugerido: ate 10 minutos.

## Pessoa 1 - Introducao e tema

Bom dia/boa noite, professor(a). Nosso projeto e um Sistema de Controle de Medicacoes Hospitalares.

A ideia do sistema e resolver um problema comum no ambiente hospitalar: organizar pacientes, setores, medicos, medicamentos e prescricoes medicas.

Com esse sistema, conseguimos registrar em qual setor o paciente esta, quais medicos fazem prescricoes e quais medicamentos estao relacionados a cada prescricao.

O projeto foi desenvolvido em ASP.NET Core com Entity Framework Core e banco MySQL.

## Pessoa 2 - Entidades e relacionamentos

O sistema possui seis entidades principais: Setor, Paciente, Medico, Medicamento, Prescricao e PrescricaoMedicamento.

Um Setor pode ter varios Pacientes, entao temos um relacionamento um para muitos.

Um Paciente pode receber varias Prescricoes, e um Medico tambem pode fazer varias Prescricoes.

O relacionamento muitos para muitos acontece entre Prescricao e Medicamento. Uma prescricao pode ter varios medicamentos, e um medicamento pode aparecer em varias prescricoes.

Para resolver esse relacionamento, usamos a tabela intermediaria PrescricaoMedicamento.

## Pessoa 3 - Estrutura e funcionalidades

Na estrutura do projeto, usamos a organizacao minima exigida: Controllers, Models e Data.

Na pasta Models ficam as classes das entidades. Na pasta Data fica o AppDbContext, responsavel pela configuracao do Entity Framework e do banco.

Na pasta Controllers ficam os endpoints da API.

Na Sprint 2, implementamos o CRUD completo, com GET, POST, PUT e DELETE para Setores, Pacientes, Medicos, Medicamentos, Prescricoes e PrescricaoMedicamentos.

Tambem adicionamos validacoes e status codes, como 400 para dados invalidos, 404 para registros nao encontrados e 409 para conflitos de exclusao.

## Pessoa 4 - Swagger e demonstracao

Agora vamos demonstrar o sistema rodando pelo Swagger.

O Swagger mostra a documentacao da API e permite testar os endpoints pelo navegador.

Primeiro, podemos testar um GET em Setores para listar os dados iniciais do banco.

Depois, podemos criar um novo setor com POST, atualizar com PUT e excluir com DELETE.

Tambem podemos demonstrar um erro, por exemplo buscando um id que nao existe, para mostrar o retorno 404.

Com isso, mostramos que a API esta funcional, documentada e pronta para a entrega final.

## Conclusao curta

Concluindo, o projeto atende aos requisitos das tres sprints: modelagem e banco na Sprint 1, CRUD completo na Sprint 2 e documentacao/apresentacao via Swagger na Sprint 3.

