# Implantação do Software

# 1. Implantação do Software 
Esta seção descreve a arquitetura técnica e o processo necessário para colocar a aplicação em funcionamento.

# 1.1. Tecnologias e Arquitetura
A aplicação é dividida em duas partes principais: o Backend (API) e o Frontend (site).

Backend (.NET 9)
O backend é construído utilizando .NET 9 e segue uma arquitetura robusta e escalável, fundamentada em padrões de projeto consolidados:

- Padrão Assíncrono: Garante alta performance e responsividade, tratando operações de longa duração (I/O) de forma assíncrona para otimizar o uso de recursos do servidor.
- Repository Pattern: Abstrai e centraliza a camada de acesso a dados (utilizando Entity Framework), tornando o código mais limpo, testável e de fácil manutenção.
- Unit of Work Pattern: Coordena múltiplas operações de escrita no banco de dados como uma única transação, garantindo a consistência e a integridade dos dados.
- Padrão DTOs (Data Transfer Objects): Otimiza a comunicação entre as camadas e a API, trafegando apenas os dados necessários e aumentando a segurança ao desacoplar as entidades de domínio.
# Outras Tecnologias:
- Autenticação: Segurança de endpoints via JWT (JSON Web Tokens).
- Documentação: Geração automática de documentação e interface de testes com Swagger UI.
- Logging: Registro de logs de eventos e erros em arquivos .txt.
- CORS: Configurado para permitir o consumo da API por clientes web de diferentes origens.
# Frontend (React Native)
O frontend é um projeto HTML, CSS e JavaScript estático.
- Tecnologia: O frontend é desenvolvido em React Native, permitindo a criação de uma experiência de usuário rica e interativa.
- Flexibilidade de Hospedagem: Por ser um projeto estático (HTML/CSS/JS), pode ser implantado em qualquer servidor web (Nginx, Apache) ou em serviços de alta performance como Vercel, Netlify ou GitHub Pages.
# 1.2. Processo de Implantação e Execução
# Backend
# Pré-requisitos:

.NET SDK 9 instalado.
# Instruções para Execução:

1. Navegue até a pasta raiz da API:
   
cd BarberConnect.Api

2. Execute o comando para iniciar o servidor:
   
dotnet run

A API estará disponível localmente. A interface do Swagger UI pode ser acessada para visualização e teste dos endpoints.

# Execução dos Testes Unitários:
A qualidade do código é garantida por uma suíte de testes unitários utilizando XUnit.

1. Navegue até a pasta de testes:
  cd BarberConnect.Api.Test

2. Execute todos os testes:
 dotnet test

3. Para executar um conjunto específico de testes (ex: ServicoTest):
 dotnet test --filter "ServicoTest"

Atualmente, a suíte conta com 70 testes, todos passando com sucesso, cobrindo as principais regras de negócio da aplicação.

# Frontend
# Instruções de Instalação
1. Clone o repositório.
2. Como o projeto é estático, basta servir os arquivos HTML, CSS e JS a partir de um servidor web ou abrir o arquivo index.html diretamente em um navegador para visualização local.

# 2. Planejamento de Evolução da Aplicação
Esta seção detalha o histórico de desenvolvimento e os planos futuros para o projeto BarberConnect.

# 2.1 Histórico de Versões

# Backend

# [0.1.0] - 05/06/2025

**Adicionado:** Estrutura inicial do projeto com template padrão, implementação dos padrões Repository, Unit of Work e DTOs, Controllers básicos, autenticação JWT, Logger, Filters, Paginação, CORS e documentação com Swagger. Adicionado template de testes com XUnit.

# Frontend

# [0.1.0] - 01/05/2025

**Adicionado:** Template inicial do projeto em React Native.

# 2.2. Roadmap e Evolução Planejada
O plano de evolução visa expandir as funcionalidades e a robustez da plataforma.

# Próximos Passos (Curto Prazo)
# Backend:
- Refinar os endpoints existentes e adicionar validações de entrada mais complexas.
- Implementar a funcionalidade de upload de imagens para perfis de barbeiros e clientes.
- Desenvolver um sistema de notificações (ex: lembretes de agendamento).
- Containerizar a aplicação com Docker para simplificar o ambiente de desenvolvimento e a implantação em produção.
  
# Frontend:
- Desenvolver as telas de Cadastro e Login para clientes e barbeiros, integrando com os endpoints de autenticação JWT.
- Construir a interface de Agendamento, permitindo que clientes visualizem horários disponíveis e marquem cortes.
- Criar o painel do barbeiro para gerenciamento de horários e visualização de agendamentos.
- Implementar o sistema de avaliação de serviços.

# Visão de Futuro (Longo Prazo)
- Integrar um gateway de pagamento para serviços premium ou pré-pagos.
- Expandir o sistema de notificações para incluir e-mail ou push notifications.
- Criar um painel administrativo para gerenciamento da plataforma.
- Otimizar a performance do banco de dados com estratégias de indexação avançada.
- Adotar um pipeline de **CI/CD (Integração e Entrega Contínua)** com GitHub Actions para automatizar os testes e o deploy em ambientes de homologação e produção.
