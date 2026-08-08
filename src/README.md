# BackEnd
### Alguns padrões de projeto usados na construção do backend.
<br>



## Padrão assíncrono.

O padrão assíncrono em uma API .NET (como no ASP.NET Core) refere-se ao uso de programação assíncrona para lidar com operações que podem levar tempo — como chamadas de banco de dados, acesso a arquivos ou chamadas HTTP externas — sem bloquear a thread do servidor que está processando a requisição.

## Padrão Repository  Patthern.
Em .NET (C#), Repository Pattern é um padrão de projeto (design pattern) usado para isolar a lógica que acessa dados da lógica de negócios da aplicação. Ele fornece uma abstração entre a camada de acesso a dados (como Entity Framework, Dapper, ADO.NET) e o restante da aplicação.

Objetivo principal:
Centralizar o acesso a dados de forma que o restante do código não precise saber como os dados são acessados, apenas que eles estão sendo acessados por meio de uma interface bem definida.

## Padrão DTOS.
Em .NET (e em outras linguagens), DTO significa Data Transfer Object — ou em português, Objeto de Transferência de Dados. É um padrão usado para transportar dados entre camadas da aplicação, sem expor diretamente as entidades do domínio (como as do Entity Framework).

Objetivo principal:
Evitar expor entidades diretamente (por exemplo, as do banco de dados) para outras camadas, como a API ou UI.

Personalizar os dados enviados ou recebidos, enviando apenas o que é necessário.

Melhorar a segurança, desempenho e controle sobre os dados trafegados.

## Padrão Unit of Work.

Em .NET, o Unit of Work (UoW) é um padrão de projeto que tem como objetivo coordenar a escrita de várias alterações em um banco de dados como uma única transação.

# Instruções de utilização do BackEnd

Deve ter o Dotnet SDK 9 instalado.
Entre na pasta BarberConnect.Api.
E digite.

```bash
dotnet run
```
Executar os testes unitários entre na pasta BarberConnect.Api.Test
e digite.
```bash
dotnet test
```


<br><br><br><br>
<br><br><br><br>

# FrontEnd
<br><br><br><br>
[escrever front aqui]

## Instalação do Site

O site em HTML/CSS/JS é um projeto estático, logo pode ser utilizado tanto em servidores...

## Histórico de versões

### backend
### [0.1.0] - 05/06/2025

#### Adicionado
- Template padrão
- Repository  Patthern
- Padrão Unit of Work
- Dtos
- Controllers
- Jwt autenticação
- Api Logger, registrar log em um .txt
- Filters
- Paginação na api
-  Services AddCors
- Swagger Ui
- Template para teste Unitario Xunit

<br><br>

### FrontEnd
### [0.1.0] - 01/05/2025
- Adicionado Templante React Native
