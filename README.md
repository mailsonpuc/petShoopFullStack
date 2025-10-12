#  Projeto Full Stack com .NET 9 + React (Vite + TypeScript)

Este é um projeto **Full Stack** desenvolvido com **.NET 9 (C#)** no back-end e **React + Vite + TypeScript** no front-end.  
A aplicação foi construída com foco em boas práticas de arquitetura, autenticação com **JWT + Identity**, e integração entre as camadas usando **DTOs**, **Repository Pattern**, e **Unit of Work**.

---

##  Arquitetura da Solução

A aplicação segue uma arquitetura em camadas:

backend

<code>
├── Context
│   └── AppDbContext.cs
├── Controllers
│   └── ProductsController.cs
├── DTOS
│   ├── Mappings
│   │   └── ProductDTO.cs
│   └── ProductDTOMappingExtensions.cs
├── Migrations
│   ├── 20251009003610_Initial.Designer.cs
│   ├── 20251009003610_Initial.cs
│   └── AppDbContextModelSnapshot.cs
├── Models
│   ├── ApplicationUser.cs
│   └── Product.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── Repositories
│   ├── Interfaces
│   │   ├── IProductRepository.cs
│   │   ├── IRepository.cs
│   │   └── IUnitOfWork.cs
│   ├── ProductRepository.cs
│   ├── Repository.cs
│   └── UnitOfWork.cs
├── appsettings.Development.json
├── appsettings.json
├── package.sh
├── petBack.csproj
├── petBack.http
├── services
│   ├── ITokenService.cs
│   └── TokenService.cs
└── wwwroot
    └── img
        ├── (01.)__31027543400.jpg
        ├── 111.jpg
        ├── 31027547421-frente.jpg
        ├── 8.webp
        ├── Areia_Higiênica_Meau_Biodegradável_Grãos_Finos_para_Gatos.jpg
        ├── Ração_Kitekat_Sachê_Peixe_para_Gatos_Adultos_1743117.jpg
        └── racao.webp
</code>

<hr>
front
<code>
├── README.md
├── eslint.config.js
├── index.html
├── package.json
├── package.sh
├── public
│   └── petshoop.jpg
├── src
│   ├── App.css
│   ├── App.tsx
│   ├── Routes.tsx
│   ├── assets
│   │   ├── PawPrintAnimation.gif
│   │   └── petshoop.jpg
│   ├── components
│   │   ├── Context
│   │   │   └── CartContext.tsx
│   │   ├── Header
│   │   │   └── index.tsx
│   │   ├── Layout
│   │   │   └── index.tsx
│   │   └── Loading
│   │       ├── index.tsx
│   │       └── loading.css
│   ├── index.css
│   ├── main.tsx
│   ├── pages
│   │   ├── Cat
│   │   │   └── index.tsx
│   │   ├── Details
│   │   │   └── index.tsx
│   │   ├── Footer
│   │   │   └── index.tsx
│   │   ├── GetImageRelativePath
│   │   │   └── index.tsx
│   │   ├── Home
│   │   │   └── index.tsx
│   │   └── Notfound
│   │       └── index.tsx
│   └── services
│       └── Api.tsx
├── tsconfig.app.json
├── tsconfig.json
├── tsconfig.node.json
├── vercel.json
├── vite.config.ts
└── yarn.lock
</code>


<hr>

---

## ⚙️ Back-end (.NET 9 Web API)

###  Tecnologias e Pacotes Utilizados

| Pacote | Versão | Função |
|--------|---------|--------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 9.0.9 | Autenticação via JWT |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 9.0.9 | Gerenciamento de usuários com Identity |
| `Microsoft.AspNetCore.OpenApi` | 9.0.9 | Suporte a documentação OpenAPI |
| `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.9 | ORM para SQL Server |
| `Microsoft.EntityFrameworkCore.Tools` | 9.0.9 | Ferramentas EF Core (migrations, etc) |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.9 | Suporte ao design-time EF Core |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | 9.0.0 | Suporte a scaffolding de controllers e views |
| `Swashbuckle.AspNetCore` | 9.0.6 | Geração automática da documentação Swagger |

###  Principais Implementações

- **Autenticação e Autorização:**
  - Implementada com **ASP.NET Identity + JWT Bearer**.
  - Tokens são emitidos no login e validados em cada requisição.

- **Padrões de Projeto:**
  - **Repository Pattern**: abstração de acesso a dados.
  - **Unit of Work**: controle de transações e persistência.
  - **DTOs e Mapping**: conversão entre entidades e objetos de transporte (usando `DTOMappingExtensions.cs`).

- **Documentação e Testes:**
  - **Swagger UI** para explorar endpoints.
  - **OpenAPI** configurado automaticamente.

- **Outros Recursos:**
  - Cache em memória com `AddMemoryCache`.
  - Armazenamento de imagens em `wwwroot/img`.

---

##  Front-end (React + Vite + TypeScript)

###  Dependências Principais

```
json
"dependencies": {
  "@tailwindcss/vite": "^4.1.14",
  "axios": "^1.12.2",
  "jwt-decode": "^4.0.0",
  "react": "^19.1.1",
  "react-dom": "^19.1.1",
  "react-hot-toast": "^2.6.0",
  "react-icons": "^5.5.0",
  "react-router-dom": "^7.9.3",
  "tailwindcss": "^4.1.14"
}
```


<hr>
Estrutura e Funcionalidades

Vite + TypeScript: build rápido e tipagem estática.

Axios: integração com a API (.NET) via requisições HTTP.

React Router v7: controle de rotas protegidas e públicas.

JWT Decode: leitura e verificação do token JWT.

TailwindCSS: estilização moderna e responsiva.

React Hot Toast: feedback visual de ações do usuário.

React Icons: ícones leves e personalizáveis.

<hr>
Autenticação e Fluxo

O usuário faz login → API gera token JWT.

O token é salvo no localStorage.

O front-end envia o token em cada requisição (Authorization: Bearer ...).

A API valida o token e autoriza o acesso aos endpoints protegidos.
<hr>
Documentação da API

Após iniciar o projeto, acesse:

Swagger UI:
https://localhost:5001/swagger
ou
http://localhost:5000/swagger

<hr>

### Como Executar o Projeto
# Entrar na pasta do backend
cd Api

# Restaurar pacotes
dotnet restore

# Aplicar migrações (se houver)
dotnet ef database update

# Executar o projeto
dotnet run

<hr>

# Front-end (React)

### Entrar na pasta do frontend
cd Frontend

### Instalar dependências
yarn install

### Executar em modo desenvolvimento
yarn run dev

<hr>
Acesse:
http://localhost:5173

Armazenamento de Imagens

As imagens utilizadas na aplicação são armazenadas em:
`wwwroot/img/`

<hr>
Esses arquivos são servidos publicamente via middleware estático do ASP.NET Core.


### Funcionalidades Principais

Autenticação e registro de usuários com Identity + JWT

CRUDs com Repository + Unit of Work

Upload e exibição de imagens (wwwroot/img)

Documentação Swagger interativa

Front-end integrado com React e Axios

Cache em memória (AddMemoryCache)

Interface moderna com TailwindCSS

