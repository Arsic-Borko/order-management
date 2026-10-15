# ADR-0001: Encapsulate Order State

**Status:** Accepted

## Context

An Order contains items and a lifecycle status. Exposing mutable collections or public status setters would allow external code to bypass business rules and create invalid states.

## Decision

The Order domain entity is responsible for maintaining its own invariants.

- Order items are stored in a private collection and exposed through a read-only interface.
- Order status can only be changed through domain methods.
- AddItem(), Confirm(), and Cancel() validate business rules before modifying state.
- OrderItem is immutable after creation, including its unit-price snapshot.

## Alternatives Considered

**Public setters and mutable collections:** Simpler to implement but allow callers to bypass business rules.

**Managing state in application services:** Keeps entities simpler but risks duplicating business rules across services.

## Consequences

**Positive:**
- Business rules are centralized.
- Invalid modifications are prevented.
- Domain behavior is explicit and easier to maintain.

**Trade-offs:**
- New operations require explicit domain methods.
- Persistence frameworks may require additional configuration for encapsulated state.