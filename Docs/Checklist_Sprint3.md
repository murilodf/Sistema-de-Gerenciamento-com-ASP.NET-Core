# Checklist da Sprint 3

## Objetivo da Sprint 3

Entregar a versao final do projeto com documentacao via Swagger/OpenAPI e apresentar o sistema funcionando para o professor.

## Itens atendidos

- API ASP.NET Core rodando.
- Banco MySQL configurado.
- Entity Framework Core configurado.
- Migration inicial reconhecida.
- Seed de dados iniciais.
- CRUD completo da Sprint 2 mantido.
- Swagger/OpenAPI configurado com titulo, versao e descricao.
- Endpoints documentados com comentarios.
- Roteiro de apresentacao preparado.

## O que mostrar na apresentacao

1. Tema do sistema e problema resolvido.
2. Entidades principais.
3. Relacionamentos 1:N e N:M.
4. Estrutura do projeto: Controllers, Models, Data, Migrations e Docs.
5. Swagger com todos os endpoints.
6. Teste de um GET.
7. Teste de um POST.
8. Teste de um PUT.
9. Teste de um DELETE.
10. Tratamento de erro, como buscar um id inexistente.

## Comandos para rodar

Iniciar MySQL, se necessario:

```powershell
Start-Process -FilePath "C:\Program Files\MySQL\MySQL Server 8.4\bin\mysqld.exe" -ArgumentList '"--defaults-file=C:\ProgramData\MySQL\MySQL Server 8.4\my.ini"' -WindowStyle Hidden
```

Rodar a API:

```powershell
dotnet run --urls http://localhost:5041
```

Abrir o Swagger:

```txt
http://localhost:5041/swagger
```

## Sugestao de testes no Swagger

- `GET /api/Setores`
- `POST /api/Setores`
- `PUT /api/Setores/{id}`
- `DELETE /api/Setores/{id}`
- `GET /api/Pacientes/999` para demonstrar `404 Not Found`

