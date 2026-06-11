# Testes de Setores - Sprint 3

Use estes testes no Swagger:

```txt
http://localhost:5041/swagger
```

## 1. Listar setores cadastrados

Endpoint:

```txt
GET /api/Setores
```

No Swagger:

1. Abra `GET /api/Setores`.
2. Clique em `Try it out`.
3. Clique em `Execute`.

Resultado esperado:

```txt
200 OK
```

Deve aparecer uma lista com setores cadastrados, como UTI e Enfermaria.

## 2. Buscar setor por id

Endpoint:

```txt
GET /api/Setores/{id}
```

Use um id existente, por exemplo:

```txt
1
```

Resultado esperado:

```txt
200 OK
```

## 3. Criar novo setor

Endpoint:

```txt
POST /api/Setores
```

Body:

```json
{
  "id": 0,
  "nome": "Pediatria",
  "andar": "3",
  "descricao": "Setor de atendimento infantil",
  "pacientes": []
}
```

Resultado esperado:

```txt
201 Created
```

Guarde o `id` retornado. Exemplo:

```json
{
  "id": 4,
  "nome": "Pediatria",
  "andar": "3",
  "descricao": "Setor de atendimento infantil",
  "pacientes": []
}
```

## 4. Atualizar setor

Endpoint:

```txt
PUT /api/Setores/{id}
```

Se o id criado foi `4`, coloque no campo `id` da rota:

```txt
4
```

Body:

```json
{
  "id": 4,
  "nome": "Pediatria Atualizada",
  "andar": "3",
  "descricao": "Setor atualizado para apresentacao",
  "pacientes": []
}
```

Resultado esperado:

```txt
204 No Content
```

## 5. Conferir atualizacao

Endpoint:

```txt
GET /api/Setores/{id}
```

Use o mesmo id atualizado:

```txt
4
```

Resultado esperado:

```txt
200 OK
```

O nome deve aparecer como:

```txt
Pediatria Atualizada
```

## 6. Excluir setor

Endpoint:

```txt
DELETE /api/Setores/{id}
```

Use o id criado:

```txt
4
```

Resultado esperado:

```txt
204 No Content
```

## 7. Testar erro 404

Depois de excluir, tente buscar o mesmo id:

```txt
GET /api/Setores/4
```

Resultado esperado:

```txt
404 Not Found
```

## Fala curta para a apresentacao

Nesta parte vamos demonstrar o CRUD de Setores. Primeiro usamos o GET para listar os setores cadastrados. Depois usamos o POST para criar um novo setor chamado Pediatria. Em seguida, usamos o PUT para atualizar esse setor. Depois usamos o DELETE para excluir. Por fim, tentamos buscar o mesmo id excluido para mostrar que a API retorna 404 quando o registro nao existe.
