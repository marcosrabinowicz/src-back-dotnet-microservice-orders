# 🧾 Orders Microservice — Neshama Tech

**Microserviço completo de Pedidos utilizando DDD + Clean Architecture + CQRS Light + EF Core**  
_By Neshama Tech — Tecnologia na veia_

---

## 📌 Visão Geral

Este microserviço implementa o fluxo completo de **Pedidos (Orders)** utilizando padrões modernos de engenharia:

- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **Use Cases explícitos**
- **Domínio rico, imutável e encapsulado**
- **Invariantes fortes**
- **Separação real de leitura e escrita (CQRS Light)**
- **EF Core otimizado**
- **Consultas performáticas**
- **API limpa e desacoplada**

O projeto serve como parte do **Plano de Especialista Backend C#** e como referência arquitetural para a Neshama Tech.

---

# 🧱 Arquitetura (Clean Architecture)

A solução segue uma estrutura clara de camadas, onde dependências sempre apontam de fora para dentro:

API → Application → Domain
API → Application → Infrastructure → DbContext

Nenhuma camada depende da que está “mais externa”.  
O domínio permanece totalmente isolado.

---

## 📁 Estrutura do Projeto

src/
├── Orders.Api
│ ├── Controllers
│ ├── Requests
│ ├── Responses
│ └── Program.cs (DI, DbContext, Routing, Swagger)
│
├── Orders.Application
│ ├── DTOs
│ ├── Interfaces
│ └── UseCases
│
├── Orders.Domain
│ ├── Aggregates
│ ├── Entities
│ ├── ValueObjects
│ └── Exceptions
│
└── Orders.Infrastructure
├── EF
│ ├── Configurations
│ └── OrdersDbContext.cs
├── Repositories
└── Queries

---

# 🧠 Domínio (Orders.Domain)

### **Aggregate Root: `Order`**
- Contém as regras centrais do pedido.
- Calcula total internamente.
- Garante invariantes:
  - Pedido não pode ter itens inválidos
  - Quantidade, preço e total são validados
  - Domínio não expõe setters

### **Entity: `OrderItem`**
- Produto de um pedido
- Sempre consistente: `Total = Quantity * UnitPrice`

### **Value Object: `Money`**
- Imutável
- Comparação por valor
- Evita manipulação incorreta de valores monetários

### **Exceptions**
- `DomainException` para violação de regras

---

# 📚 Application (Orquestração)

Camada responsável por coordenar o fluxo entre API, Domínio e Infraestrutura.

### 🔹 DTOs (Input/Output)
- `CreateOrderInput`
- `CreateOrderItemInput`
- `CreateOrderOutput`
- `OrderListItemOutput`
- `OrderDetailOutput`
- `OrderDetailItemOutput`

### 🔹 Interfaces
- `IOrderRepository` → escrita (aggregate root)
- `IOrderQuery` → leitura (projeções otimizadas)

### 🔹 UseCases (Command Side)
- `CreateOrderUseCase`
  - Valida DTO
  - Cria `Order` usando o domínio
  - Persiste via `IOrderRepository`
  - Retorna `CreateOrderOutput`

---

# ⚙️ Infraestrutura (Orders.Infrastructure)

Responsável por acesso a dados, EF Core e persistência.

### 🔹 DbContext
`OrdersDbContext`
- DbSet<Order>
- DbSet<OrderItem>
- Mapeamentos aplicados via Fluent Configuration

### 🔹 Configurações EF Core
- `OrderConfiguration`
- `OrderItemConfiguration`

Com:
- Tipos
- Tamanhos
- Foreign Keys
- Índices
- Regras SQL-friendly

### 🔹 Repository (Write Model)
`OrderRepository`
- Reconstrói Aggregate completo
- Usa Include apenas no contexto adequado (comando)

### 🔹 Queries (Read Model — CQRS Light)
`OrdersQueries`
- Consultas performáticas usando:
  - AsNoTracking
  - Projeção via Select
  - Paginação (Skip/Take)
  - Total calculado no SQL

Implementa:
- `ListAsync(page, pageSize)`
- `GetByIdAsync(id)`

---

# 🌐 API (Orders.Api)

### 🔹 Requests
- `CreateOrderRequest`
- `CreateOrderItemRequest`

### 🔹 Responses
- `OrderListItemResponse`
- `OrderDetailResponse`

### 🔹 Controller
`OrdersController`

Endpoints implementados:

#### **POST /orders**
Cria um pedido.  
Fluxo: Request → Input → UseCase → Repository → Output → Response.

#### **GET /orders**
Lista paginada de pedidos (DTO projetado — leitura).

#### **GET /orders/{id}**
Detalhe completo de um pedido via Query (CQRS Light).

---

# 🔌 Injeção de Dependências (DI)

`Program.cs` configura:

- `AddDbContext<OrdersDbContext>`
- `AddScoped<ICreateOrderUseCase, CreateOrderUseCase>`
- `AddScoped<IOrderRepository, OrderRepository>`
- `AddScoped<IOrderQuery, OrdersQueries>`
- Swagger
- Controllers
- CORS

### Banco configurado:
**EF Core InMemory (provider 8.x compatível com .NET 8)**  
Pronto para ser trocado por Postgres ou SQL Server sem quebrar camadas.

---

# 🎯 Pilares Técnicos Aplicados

- DDD com Aggregate Root real
- Encapsulamento forte e imutabilidade
- Clean Architecture aplicada corretamente
- CQRS Light com separação total de leitura/escrita
- API desacoplada de Domain + Infra
- Projeções leves e performáticas
- Domínio isolado e blindado contra efeitos externos
- Codebase preparada para testes unitários e de integração

---

# 🚀 Objetivos Alcançados na Semana 1

- Arquitetura estabelecida com clareza  
- Fluxo completo criado com correção e propósito  
- Microserviço funcionando ponta-a-ponta  
- Todas as camadas alinhadas  
- Consultas performáticas  
- Regras de domínio consolidadas  
- API limpa e profissional  

Pronto para entrar na **Semana 2**:  
**CQRS avançado, Azure e Observabilidade.**

---

# 🧑‍💻 Desenvolvido por  
**Marcos Rabinowicz — Neshama Tech**  
_Tecnologia na veia. Propósito no código._
