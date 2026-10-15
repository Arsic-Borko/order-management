# ADR-0002: Enforce Order Lifecycle Transitions

**Status:** Accepted

## Context

Orders have a defined lifecycle. Unrestricted status changes could allow invalid transitions, such as modifying a confirmed order or confirming an order without items.

## Decision

The Order entity enforces the following state transitions:

- Pending → Confirmed, only if the order contains at least one item.
- Pending → Cancelled, including empty orders.

Confirmed and Cancelled are terminal states.

Items can only be added while an order is Pending. Invalid transitions throw InvalidOperationException, and validation occurs before state mutation.

## Alternatives Considered

**Direct status assignment:** Simple but does not protect lifecycle invariants.

**Lifecycle management in application services:** Risks inconsistent validation across different callers.

**Dedicated state-machine library:** Adds unnecessary complexity for the current lifecycle.

## Consequences

**Positive:**
- Valid transitions are explicit.
- Terminal states are protected.
- Business rules are enforced consistently.

**Trade-offs:**
- Introducing new statuses requires reviewing transition rules.
- Application boundaries must translate domain exceptions into appropriate responses.