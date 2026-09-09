# PetShoop FrontEnd

Frontend da aplicação PetShoop, construído com **React 19 + TypeScript + Vite + Tailwind CSS**.

## Stack

| Tecnologia | Uso |
|---|---|
| React 19 | Biblioteca de UI e gerenciamento de estado |
| TypeScript | Tipagem estática em todo o código |
| Vite 8 | Build tool, HMR em desenvolvimento e otimização de produção |
| Tailwind CSS 4 | Framework de estilização utility-first |
| React Router DOM 7 | Rotas do lado do cliente |
| Axios | HTTP client para chamadas à API |
| React Icons | Ícones vetoriais |
| ESLint 10 + TypeScript-ESLint | Linting com regras tipadas |

## Padrões de Arquitetura

### Estrutura de pastas

```
src/
├── Components/        # Componentes reutilizáveis (ProtectedRoute, Modal, PaginationControls, ConfirmDialog)
├── Contexts/          # Contextos React (AuthContext)
├── Hooks/             # Custom hooks (useCrud, useAdminRole)
├── Services/          # Camada de acesso à API (Api.ts, api.ts)
├── Types/             # Tipos e contratos de dados compartilhados
├── Assets/            # Imagens e assets estáticos
├── App.tsx            # Componente raiz com rotas
├── main.tsx           # Entry point da aplicação
├── index.css          # Estilos globais
└── App.css            # Estilos específicos do App
```

### Padrão Repository / Service

A camada `Services/api.ts` atua como um repositório de dados, centralizando todas as chamadas HTTP para a API .NET. Cada domínio (Cliente, Pet, Funcionario, etc.) possui seu próprio objeto de API com as operações CRUD:

- ` clientesApi` – list, getPaged, getById, create, update, delete
- `petsApi`, `funcionariosApi`, `produtosApi`, `servicosApi`, `agendamentosApi`, `consultasApi`, `vacinasApi`, `prontuariosApi`, `vendasApi`, `itemVendasApi`
- `dashboardApi` – dados do painel
- `authApi` – login e registro

### Hook customizado de CRUD (`useCrud.ts`)

Hook reutilizável que abstrai o estado (itais, loading, erro, paginação) e as operações de CRUD. Suporta tanto listagem simples quanto paginação server-side (`fetchPagedFn`).

### Context API para autenticação (`AuthContext.tsx`)

Gerencia o estado de autenticação (usuário, token, loading) e persiste os dados no `localStorage`. Fornece `login`, `logout`, `isAuthenticated` e `user` para qualquer componente.

### Proteção de rotas (`ProtectedRoute.tsx`)

- `ProtectedRoute` – redireciona para `/login` se não autenticado.
- `AdminProtectedRoute` – redireciona para `/sem-permissao` se o usuário não for administrador.

### Estado e paginação

A paginação é tratada de forma unificada: o tipo `PagedResponse<T>` contém `data` e `pagination` (`totalCount`, `pageSize`, `currentPage`, `totalPages`, `hasNextPage`, `hasPreviousPage`). O componente `PaginationControls` exibe os controles de página.

## Configuração

### Dependências

```bash
yarn install
```

### Variáveis de ambiente

A API .NET é consumida via `Services/Api.ts`. Em desenvolvimento, aponte para a URL desejada (ex: `http://localhost:5100`). Em produção, use variáveis de ambiente ou configuração do Vite (`vite.config.ts`).

## Comandos

### Desenvolvimento

```bash
yarn dev
```

Inicia o servidor de desenvolvimento com HMR (Hot Module Replacement).

### Build de produção

```bash
yarn build
```

Gera a pasta `dist/` com a aplicação otimizada para produção.

### Lint

```bash
yarn lint
```

Executa o ESLint em todo o projeto.

### Preview da build

```bash
yarn preview
```

Inicia um servidor estático para visualizar a build de produção.