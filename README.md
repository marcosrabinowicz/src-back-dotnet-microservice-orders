# 🧾 Orders Microservice

**Microserviço de pedidos desenvolvido com DDD + Clean Architecture + C#**  
_By Neshama Tech — Tecnologia na veia_

---

## 📌 Visão Geral

Este microserviço implementa um sistema de **Pedidos (Orders)** utilizando fundamentos de engenharia de software de alto nível:

- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **Use Cases explícitos**
- **Domínio rico e encapsulado**
- **Invariantes fortes**
- **Eficiência e escalabilidade**
- **Preparado para EF Core otimizado e SQL avançado**

O objetivo deste projeto é servir como base sólida para microserviços reais, além de ser um estudo avançado dentro do plano **Especialista Backend C#**.

---

## 🧱 Arquitetura

A solução segue a Clean Architecture, separando as camadas em:

/src
/Orders.Domain
/Aggregates
/Entities
/ValueObjects
/Exceptions
/Services

/Orders.Application
/DTOs
/UseCases
/Interfaces

/Orders.Infrastructure (↳ será implementado no Dia 6)
/EF
/Repositories

/Orders.API (↳ será implementado no Dia 6)
/Controllers
/Requests
/Responses

---

## 🧠 Domain Layer (Regras de Negócio)

### **Aggregate Root: `Order`**

- Responsável por manter invariantes e consistência.
- Contém:
  - `CustomerId`
  - `Items`
  - Total calculado internamente
- Protege a consistência com `EnsureInvariants()`.

### **Entity: `OrderItem`**

- Imutável, com:
  - `ProductId`
  - `Quantity`
  - `UnitPrice`
  - `Total = Quantity * UnitPrice`

### **Value Object: `Money`**

- Imutável
- Suporta operadores
- Valida moedas e valores
- Igualdade por valor

### **Exceptions**

- `DomainException` para validação de regras

---

## 📚 Application Layer (Casos de Uso)

Camada de orquestração da aplicação.
Não possui regra de negócio — apenas coordena chamadas ao domínio e repositórios.

### **UseCase principal**

`CreateOrderUseCase`

- Recebe DTOs de entrada (`CreateOrderInput`)
- Cria Order via domínio
- Aplica regras internas via Aggregate Root
- Persiste via repositório (`IOrderRepository`)
- Retorna DTO simples (`CreateOrderOutput`)

### **DTOs**

- `CreateOrderInput`
- `CreateOrderItemInput`
- `CreateOrderOutput`

### **Interfaces**

- `IOrderRepository`

---

## ⚙️ Próximas Etapas (Plano de Execução)

### **Dia 6 — Infrastructure + EF Core + API**

- Implementar `OrdersDbContext`
- Mapear Order e OrderItem corretamente
- Criar configurações com Fluent API
- Criar índices adequados
- Implementar o repositório concreto
- Implementar os endpoints:
  - `POST /orders`
  - `GET /orders/{id}`
  - `GET /orders?page=1&pageSize=20`
- Utilizar projeção (Select) para máxima performance

---

## 🔍 Pilares Técnicos utilizados até agora

- DDD orientado a invariantes
- Clean Architecture aplicada
- Encapsulamento forte do domínio
- Zero regra de negócio fora do Aggregate Root
- Uso de DTOs para fronteiras externas
- Repositórios como abstração
- Preparação para EF Core otimizado
- Preparação para SQL de alta performance

---

## 🚀 Objetivo Final

Criar um microserviço robusto, escalável e moderno, seguindo exatamente os padrões usados por grandes empresas:

- Domínio isolado
- Infra descartável
- API limpa
- Queries otimizadas
- Concorrência controlada
- Estrutura fácil de manter e evoluir

Este projeto também será utilizado como **material de estudo**, **portfólio profissional** e referência para os demais sistemas da Neshama Tech.

---

## 🧑‍💻 Desenvolvido por

**Marcos Rabinowicz — Neshama Tech**  
_Tecnologia na veia. Propósito no código._
