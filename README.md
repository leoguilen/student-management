# Gerenciamento de Alunos - API
Esse projeto é um sistema de gerenciamento de alunos, materias e notas, onde é possível cadastrar, editar, excluir e visualizar essas informações. O sistema foi desenvolvido em C# com o uso do .NET 8.0 e Entity Framework Core.

## Funcionalidades
- Autenticação e autorização com JWT
- CRUD de Alunos
- CRUD de Materias
- CRUD de Notas
- Visualização de notas por aluno

## Endpoints
- Autenticação
  - POST /api/auth/token
- Alunos
    - GET /api/students - Listar alunos
    - GET /api/students/{id} - Buscar aluno por id
    - POST /api/students - Cadastrar aluno
    - PUT /api/students/{id} - Atualizar aluno
    - DELETE /api/students/{id} - Deletar aluno
    - GET /api/students/{id}/grades - Listar notas de um aluno
- Materias
    - GET /api/subjects - Listar materias
    - GET /api/subjects/{id} - Buscar materia por id
    - POST /api/subjects - Cadastrar materia
    - PUT /api/subjects/{id} - Atualizar materia
    - DELETE /api/subjects/{id} - Deletar materia
- Notas
    - POST /api/grades - Cadastrar nota
    - PUT /api/grades/{id} - Atualizar nota
    - DELETE /api/grades/{id} - Deletar nota

## Como executar
### Pré-requisitos
- .NET 8.0 SDK

### Passos
1. Clone o repositório na sua máquina local
```bash
git clone https://github.com/leoguilen/student-management.git
```
2. Acesse a pasta do projeto
```bash
cd student-management
```

3. Execute o projeto
```bash
dotnet run --project src/StudentManagement.Api
```

4. Acesse a URL `https://localhost:5066/swagger` para visualizar a documentação da API

5. Utilize o [endpoint de autenticação](https://localhost:5066/api/auth/token) para obter um token JWT e utilize-o para acessar os demais endpoints

## Como testar
1. Execute o comando abaixo para rodar os testes unitários
```bash
dotnet test --filter Category=Unit
```
2. Execute o comando abaixo para rodar os testes de integração
```bash
dotnet test --filter Category=Integration
```