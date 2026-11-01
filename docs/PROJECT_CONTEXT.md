# Order Management — Project Context

> This document provides a concise snapshot of the project's current state for developers and AI-assisted development sessions.
>
> Update it whenever a significant milestone is completed or the immediate development focus changes.

## 1. Project Overview

**Project:** Order Management

**Repository:** https://github.com/Arsic-Borko/order-management

**Technology:** C# / .NET 10

**Architecture:** Layered architecture inspired by Clean Architecture principles.

**Current stage:** Early development — Application and API foundation.

**Long-term direction:** A potential technology product, with particular interest in intelligent logistics, automation, algorithms, and performance engineering.

No specific commercial product or customer segment has been validated.

For additional context, see:

- [Vision](VISION.md)
- [Architecture](ARCHITECTURE.md)
- [Roadmap](ROADMAP.md)

## 2. Solution Structure

```text
order-management/
├── src/
│   ├── OrderManagement.Api/
│   ├── OrderManagement.Application/
│   └── OrderManagement.Domain/
├── tests/
│   └── OrderManagement.Tests/
├── docs/
│   ├── adr/
│   ├── VISION.md
│   ├── ARCHITECTURE.md
│   ├── ROADMAP.md
│   └── PROJECT_CONTEXT.md
└── OrderManagement.slnx
```

The Infrastructure project has not yet been created.

## 3. Current Implementation

### Domain

**Order**

- Identified by a non-empty `Guid`.
- Starts in the `Pending` state.
- Supports adding items only while pending.
- Can be confirmed only while pending and containing at least one item.
- Can be cancelled only while pending.
- Confirmed and cancelled states are terminal.
- Calculates `TotalPrice` from its items.
- Exposes items as a read-only collection.

**OrderItem**

- Requires a non-empty product identifier.
- Requires quantity of at least 1.
- Requires non-negative unit price.
- Calculates `LineTotal` from quantity and unit price.

**OrderStatus**

- `Pending`
- `Confirmed`
- `Cancelled`

### Application

**IOrderRepository**

Currently defines:

```csharp
Task AddAsync(Order order);
```

**OrderService**

Implements `CreateOrderAsync()`:

1. Generates a new order identifier.
2. Creates an `Order` domain object.
3. Persists the order through `IOrderRepository`.
4. Returns the generated identifier.

**IOrderService**

Defines the application service contract.

### API

The API project exists, but the order creation endpoint has not yet been implemented.

### Infrastructure

Not yet implemented.

The next planned component is `InMemoryOrderRepository`.

## 4. Testing

**Frameworks:**

- NUnit
- NSubstitute

**Existing coverage:**

- Domain invariants and constructor validation.
- Order lifecycle transitions.
- Application-level order creation.
- Verification that the repository receives the created order.

Tests should be maintained alongside the corresponding implementation.

## 5. Architectural Decisions

Existing ADRs:

- `0001-encapsulate-order-state.md`
- `0002-enforce-order-lifecycle-transitions.md`
- `0003-calculate-order-total-from-items.md`

Important principles:

- Keep business invariants in the Domain layer.
- Use application services to coordinate use cases.
- Depend on repository abstractions rather than concrete implementations.
- Introduce infrastructure incrementally.
- Avoid unnecessary frameworks and architectural patterns.
- Consider algorithmic complexity when choosing data structures.
- Validate performance assumptions through measurement.

## 6. Git Workflow

Development uses feature branches and Pull Requests.

**Main branch:** `main`

**Feature branch:** `feature/create-order-use-case`

**Documentation branch:** `docs/project-foundation`

The CreateOrder feature branch has been pushed to GitHub but has not yet been merged into `main`.

The documentation branch is intended to contain the initial project documentation and be merged through a separate Pull Request.

Before starting a new development task:

1. Verify the current branch and working tree.
2. Confirm the intended base branch.
3. Create or switch to the appropriate feature branch.
4. Implement and test the change.
5. Commit and push.
6. Open a Pull Request when the feature is ready.

## 7. Immediate Next Task

**Implement `InMemoryOrderRepository`.**

Expected approach:

1. Introduce the Infrastructure project.
2. Reference the Application project.
3. Implement the `IOrderRepository` contract.
4. Evaluate suitable in-memory data structures.
5. Discuss expected time and space complexity.
6. Add appropriate tests.

Do not introduce a database, CQRS framework, or messaging infrastructure at this stage.

## 8. Learning and Collaboration Preferences

The project is intended to support active learning rather than passive code generation.

Preferred approach:

- Discuss design decisions before implementation.
- Explain trade-offs and underlying principles.
- Encourage independent implementation.
- Review code and tests together.
- Avoid providing complete solutions prematurely.
- Integrate algorithms and performance analysis into real project tasks.

Relevant learning resources:

- *The Pragmatic Programmer*
- *Cracking the Coding Interview*, 6th Edition
- *Designing Data-Intensive Applications* (planned)

## 9. Context Maintenance

Update this document when:

- A development milestone is completed.
- A new architectural component is introduced.
- Significant decisions change.
- A feature branch is merged.
- The immediate next task changes.

This document is a snapshot, not the authoritative specification of the entire system.

The source code, automated tests, ADRs, and other project documentation remain the primary references.