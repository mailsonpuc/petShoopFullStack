# Implantação

## Visão Geral

A aplicação PetShoop é dividida em duas partes que são implantadas em serviços cloud distintos:

- **Backend (.NET 10 API)** → implantado no **Azure** (App Service / SQL Database)
- **Frontend (React + TypeScript + Vite)** → implantado no **Vercel**

---

## 1. Implantação do Backend no Azure

### Tecnologias e arquitetura

O backend é construído em .NET 10 com os seguintes padrões:

- **Assíncrono** – operações de I/O sem bloquear threads do servidor.
- **Repository Pattern** – abstração da camada de acesso a dados (Entity Framework).
- **Unit of Work** – coordena múltiplas escritas em uma única transação.
- **DTOs** – objetos de transferência que expõem apenas o necessário.
- **Autenticação JWT** – tokens de acesso assinados com `HmacSha256`.
- **Swagger UI** – documentação interativa dos endpoints.
- **Prometheus + Health Checks** – métricas e monitoramento.

### Pré-requisitos

- .NET 10 SDK instalado.
- Azure CLI (`az`) configurado e logado (`az login`).
- Banco de dados SQL Server disponível no Azure (ou SQL Database).

### Configuração de variáveis de ambiente

As strings de conexão e o segredo JWT **não devem** ser commitados. No `appsettings.json` eles estão vazios e são preenchidos via variáveis de ambiente (o ASP.NET Core reconhece o padrão `__` para aninhamento):

```bash
export ConnectionStrings__DefaultConnection="Server=<servidor>;Database=<banco>;User=<usuario>;Password=< senha>;TrustServerCertificate=True;"
export Jwt__SecretKey="<sua-secret-key>"
```

Em Azure App Service, adicione essas mesmas variáveis no painel **Configuração → Configurações de aplicativo**.

### Comandos locais (desenvolvimento)

```bash
cd src/backend
dotnet run --project PetShoop.API
```

### Comandos de migrations

```bash
dotnet ef migrations add Inicial \
  --project PetShoop.Infrastructure \
  --startup-project PetShoop.API \
  --context AppDb Context

dotnet ef database update \
  --project PetShoop.Infrastructure \
  --startup-project PetShoop.API \
  --context AppDb Context
```

### Deploy no Azure App Service

```bash
az webapp deploy \
  --resource-group <nome-do-grupo-de-recursos> \
  --name <nome-do-app-service> \
  --src-path . \
  --build-framework "dotnet" \
  --output-target "zip"
```

Ou use o Visual Studio / VS Code com a extensão **Azure App Service** para publicar diretamente.

---

## 2. Implantação do Frontend no Vercel

### Stack

- React 19 + TypeScript
- Vite 8 (build tool)
- Tailwind CSS 4
- React Router DOM 7
- Axios
- ESLint 10

### Pré-requisitos

- Node.js 18+ instalado.
- Conta no [Vercel](https://vercel.com) (gratuita para projetos pessoais).
- Vercel CLI instalada: `npm i -g vercel`

### Instalação de dependências

```bash
cd src/frontend/PetShoopFront
yarn install
```

### Comandos locais

```bash
# Desenvolvimento (HMR)
yarn dev

# Build de produção
yarn build

# Preview da build
yarn preview

# Lint
yarn lint
```

### Configuração da API

A URL da API .NET deve ser configurada no `vite.config.ts` ou via variável de ambiente (ex: `.env.local` com `VITE_API_BASE_URL`).

### Deploy no Vercel

```bash
cd src/frontend/PetShoopFront
vercel
```

Para produção:

```bash
vercel --prod
```

Ou conecte o repositório Git ao Vercel para **deploy automático** em cada push para `main`.

### Configuração do Vercel (vercel.json)

```json
{
  "buildCommand": "yarn build",
  "outputDirectory": "dist",
  "installCommand": "yarn install"
}
```

---

## 3. Fluxo de CI/CD (Recomendado)

- **GitHub Actions**: automatize testes unitários (`.NET`) e build do frontend em cada pull request.
- **Azure Pipelines** ou **Vercel Git Integration**: faça deploy automático em staging/produção.
- Use **segredos** (Azure Key Vault, Vercel Environment Variables) para armazenar `Jwt__SecretKey` e `ConnectionStrings__DefaultConnection` em produção.