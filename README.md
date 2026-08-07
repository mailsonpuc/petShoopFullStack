# PetShoop

Aplicação full stack para gestão de pet shop, com API em .NET 10 e arquitetura em camadas.

## Sobre
Este projeto possui:
- API REST com ASP.NET Core
- Identity para autenticação e autorização
- Entity Framework Core com SQL Server
- Swagger para documentação dos endpoints
- Repositórios e serviços organizados por domínio

## Tecnologias
- .NET SDK 10.0.301
- ASP.NET Core
- Entity Framework Core 10
- SQL Server
- Swagger / OpenAPI
- JWT

## Arquitetura
A solução está organizada em camadas:
- PetShoop.API: camada de apresentação
- PetShoop.Application: serviços e DTOs
- PetShoop.Domain: entidades, interfaces e regras de negócio
- PetShoop.Infrastructure: contexto, repositórios, migrations e acesso ao banco
- PetShoop.CrossCutting: injeção de dependência e configurações transversais

## Requisitos
Antes de rodar o projeto, certifique-se de ter:
- .NET SDK 10 instalado
- SQL Server em execução
- Git

## Como executar

### 1. Clonar e entrar na pasta do projeto
```bash
git clone <repo-url>
cd petShoopFullStack
```

### 2. Configurar a conexão com o banco
O arquivo de configuração da API está em:
- PetShoop.API/appsettings.json

A string de conexão padrão usada é:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Database=clearArchitecture;User=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;"
}
```

Certifique-se de que o SQL Server esteja acessível nessa instância e que o banco exista ou seja criado pelas migrations.

## Migrations

### Criar uma nova migration
```bash
dotnet ef migrations add Inicial --project PetShoop.Infrastructure --startup-project PetShoop.API --context AppDbContext
```

### Aplicar as migrations no banco
```bash
dotnet ef database update --project PetShoop.Infrastructure --startup-project PetShoop.API --context AppDbContext
```

Se quiser visualizar as migrations existentes:
```bash
dotnet ef migrations list --project PetShoop.Infrastructure --startup-project PetShoop.API --context AppDbContext
```

## Rodar a API
```bash
dotnet run --project PetShoop.API
```

A API será iniciada e o Swagger estará disponível na raiz do projeto, conforme a configuração do Program.cs.

## JWT
As configurações de JWT estão em:
- PetShoop.API/appsettings.json

Os valores usados incluem:
- SecretKey
- Issuer
- Audience
- ExpireMinutes

## Swagger
A API expõe documentação Swagger automaticamente em modo de desenvolvimento.

## Endpoints
Os principais endpoints estão organizados por controller, como:
- Clientes
- Pets
- Agendamentos
- Consultas
- Funcionarios
- Produtos
- Servicos
- Vendas
- Vacinas
- Prontuarios

## Exemplos de requests
Exemplos de requisições podem ser adicionados no arquivo de testes HTTP ou via Swagger.

## Exemplos de responses
As respostas seguem o padrão JSON retornado pelos controllers da API.

## Testes
Para executar os testes do projeto:
```bash
dotnet test
```

## Frontend
A camada de frontend ainda não está implementada nesta documentação; a API pode ser consumida diretamente.

## Docker
Se você quiser rodar o SQL Server em container, pode usar:
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

## Screenshots
Adicione capturas da aplicação aqui quando necessário.

## Roadmap
- Melhorar autenticação e autorização
- Expandir cobertura de testes
- Adicionar frontend
- Melhorar documentação dos endpoints

Exemplo do banco de dados:
```text
                    ┌──────────┐
                    │ Cliente  │
                    └────┬─────┘
                         │ 1
                         │
                         │ N
                    ┌────▼─────┐
                    │   Pet    │
                    └────┬─────┘
                         │
              ┌──────────┼───────────┐
              │          │           │
              ▼          ▼           ▼
        Agendamento    Vacina     Consulta
              │                      │
        ┌─────┴─────┐          ┌─────┴─────┐
        ▼           ▼          ▼           ▼
     Serviço   Funcionário   Pet       Funcionário
```

```text
Cliente
   │
   │ 1:N
   ▼
 Venda
   │
   │ 1:N
   ▼
ItemVenda
   │
   │ N:1
   ▼
Produto
```
