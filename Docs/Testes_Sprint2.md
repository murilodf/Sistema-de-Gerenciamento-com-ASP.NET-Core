# Testes da Sprint 2 via Swagger

Abra o Swagger em:

```txt
http://localhost:5041/swagger
```

## Endpoints CRUD

Testar os seguintes recursos:

- Setores
- Pacientes
- Medicos
- Medicamentos
- Prescricoes
- PrescricaoMedicamentos

Cada recurso possui:

- GET para listar registros
- GET por id para consultar um registro especifico
- POST para criar um registro
- PUT para atualizar um registro
- DELETE para excluir um registro

## Exemplos para teste

### Criar setor

Endpoint:

```txt
POST /api/Setores
```

Body:

```json
{
  "nome": "Pediatria",
  "andar": "3",
  "descricao": "Setor de atendimento infantil"
}
```

Resultado esperado:

```txt
201 Created
```

### Atualizar setor

Endpoint:

```txt
PUT /api/Setores/{id}
```

Body:

```json
{
  "id": 3,
  "nome": "Pediatria Atualizada",
  "andar": "3",
  "descricao": "Setor atualizado"
}
```

Resultado esperado:

```txt
204 No Content
```

### Excluir setor

Endpoint:

```txt
DELETE /api/Setores/{id}
```

Resultado esperado:

```txt
204 No Content
```

## Validacoes e erros

Exemplos de status codes:

- 400 Bad Request: quando o id da rota nao bate com o id do body ou quando uma chave estrangeira nao existe.
- 404 Not Found: quando o registro pesquisado nao existe.
- 409 Conflict: quando tenta excluir um registro que possui relacionamento com outro registro.

## Observacao

Para testar Pacientes, Prescricoes e PrescricaoMedicamentos, use ids que ja existem no banco inicial:

- Setor: 1 ou 2
- Medico: 1 ou 2
- Paciente: 1 ou 2
- Medicamento: 1 ou 2
- Prescricao: 1
