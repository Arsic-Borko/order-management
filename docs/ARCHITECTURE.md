# Order Management — Architecture

## 1. Overview

Order Management is a .NET application designed around clear separation of responsibilities, explicit domain rules, and incremental architectural evolution.

The project follows a layered architecture inspired by Clean Architecture principles.

The current objective is to establish a maintainable foundation without introducing unnecessary infrastructure or architectural complexity.

The architecture will evolve in response to actual requirements rather than anticipated complexity.

## 2. Solution Structure

The solution is organized into the following projects:

```text
OrderManagement.slnx
├── src/
│   ├── OrderManagement.Api/
│   ├── OrderManagement.Application/
│   └── OrderManagement.Domain/
├── tests/
│   └── OrderManagement.Tests/
└── docs/
    └── adr/
```

An Infrastructure project is planned but has not yet been introduced.

### Domain Layer

`OrderManagement.Domain`

Contains the core business model, business rules, and invariants.

Current domain components:

- `Order`
- `OrderItem`
- `OrderStatus`

Responsibilities:

- Enforce valid domain state
- Protect business invariants
- Control order lifecycle transitions
- Calculate order totals
- Remain independent of infrastructure and application concerns

The `Order` aggregate controls modifications to its items and status through explicit domain methods.

### Application Layer

`OrderManagement.Application`

Coordinates application use cases and defines contracts for external dependencies.

Current components:

- `OrderService`
- `IOrderService`
- `IOrderRepository`

The `CreateOrderAsync()` use case creates a new order, delegates persistence through the repository abstraction, and returns the generated order identifier.

The Application layer depends on the Domain layer.

### API Layer

`OrderManagement.Api`

The API project serves as the HTTP entry point.

It will be responsible for:

- Exposing REST endpoints
- Handling HTTP requests and responses
- Translating transport-level data into application operations
- Configuring dependency injection

Order-related REST endpoints have not yet been implemented.

Business rules should remain outside API controllers or endpoint handlers.

### Infrastructure Layer (Planned)

`OrderManagement.Infrastructure`

Will provide implementations of interfaces defined by the Application layer.

The first planned implementation is an in-memory order repository.

A persistent database implementation may be introduced when required.

## 3. Dependency Direction

Dependencies should point toward the core business model.

```text
API ──────────────► Application ──────► Domain
                         ▲
                         │
                   Infrastructure
```

The Infrastructure layer will implement contracts defined in Application.

The API composition root may reference Infrastructure to register concrete dependencies.

The Domain layer must not depend on Application, API, or Infrastructure.

## 4. Domain Design

### Order Aggregate

`Order` is the aggregate root responsible for maintaining the consistency of its items and lifecycle.

An order starts in the `Pending` state.

Supported transitions:

- `Pending` → `Confirmed`
- `Pending` → `Cancelled`

Confirmed and cancelled orders are currently terminal states.

An order cannot be confirmed without at least one item.

Items can only be added while the order is pending.

### OrderItem

An order item must satisfy the following invariants:

- Product identifier must not be empty
- Quantity must be at least 1
- Unit price must not be negative

The line total is calculated from quantity and unit price.

### Derived Values

The order total is calculated from its items rather than maintained as separately mutable state.

This avoids unnecessary synchronization between stored totals and individual order items.

## 5. Testing Strategy

Automated tests are developed alongside implementation.

Current coverage includes:

- Domain constructor validation
- Domain state transitions
- Domain invariants
- Application use-case behavior
- Verification of repository interactions

Technologies:

- NUnit
- NSubstitute

Future testing may include repository tests and API integration tests.

Tests should verify meaningful behavior rather than implementation details whenever possible.

## 6. Algorithms and Performance

Algorithmic efficiency is considered part of the engineering process.

When selecting data structures or implementing algorithms, we evaluate:

- Expected operation complexity
- Time and space trade-offs
- Expected data volumes
- Read/write access patterns
- Maintainability and clarity

The planned in-memory repository will provide an opportunity to compare alternative collection types.

Performance optimizations should be supported by measurements when practical.

BenchmarkDotNet may be introduced for focused experiments.

## 7. Architectural Principles

### Simplicity First

Prefer the simplest design that satisfies current requirements.

Avoid premature introduction of microservices, messaging infrastructure, or complex architectural patterns.

### Explicit Domain Behavior

Business rules belong in the domain model and should not be duplicated across layers.

### Dependency Inversion

Application logic depends on abstractions rather than concrete infrastructure implementations.

### Incremental Evolution

Introduce new components when justified by concrete use cases.

### Measurable Performance

Evaluate algorithmic complexity during design and use profiling or benchmarking to validate performance assumptions.

### Documented Decisions

Significant architectural decisions are captured in Architecture Decision Records (ADRs).

## 8. Current Architectural Status

Implemented:

- Domain model and lifecycle rules
- Application-level CreateOrder use case
- Repository abstraction
- Domain and application unit tests

Planned next steps:

1. Implement an in-memory repository.
2. Expose the CreateOrder use case through an HTTP endpoint.
3. Introduce API integration tests.
4. Evaluate subsequent use cases based on domain and product requirements.

The architecture is intentionally minimal and will evolve as the project grows.