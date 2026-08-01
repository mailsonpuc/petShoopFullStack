# Projeto Full Stack com .NET 10 

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
