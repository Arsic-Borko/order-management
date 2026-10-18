# ADR-0003: Calculate Order Total from Order Items

**Status:** Accepted

## Context

An Order contains a collection of OrderItems, each providing a LineTotal calculated from its quantity and unit price.

The Order must expose its total price while ensuring that the value remains consistent when items are added or, potentially, removed in the future.

We considered two approaches:

- Store TotalPrice as mutable state and update it whenever the collection changes.
- Calculate TotalPrice dynamically from the current collection of OrderItems.

## Decision

We will implement TotalPrice as a calculated, read-only property within the Order domain entity.

```csharp
public decimal TotalPrice => _items.Sum(item => item.LineTotal);
```

The collection of OrderItems remains the single source of truth for the order's total.

The Order entity owns this calculation because it already contains all the information required to perform it.

## Alternatives Considered

**Store TotalPrice as mutable state**

This would make reading the total inexpensive, but every operation that modifies the collection would also need to update the stored value.

Missing an update could result in inconsistent data.

**Introduce a dedicated price calculation service**

A separate service could be useful for complex pricing rules involving external information, promotions, or discount policies.

However, introducing such a service for a simple sum would add unnecessary complexity.

## Consequences

**Positive:**
- No duplicated mutable state.
- TotalPrice always reflects the current OrderItems collection when accessed.
- Future item removal would not require additional total-price synchronization logic.
- The calculation remains encapsulated within the domain entity.

**Trade-offs:**
- Each access to TotalPrice iterates through the collection, resulting in O(n) time complexity.
- More complex pricing requirements may require revisiting this design.

## Related Principles

**DRY (Don't Repeat Yourself):** Avoid maintaining the same business information in multiple places.

**ETC (Easier to Change):** Changes to the OrderItems collection do not require additional synchronization logic for TotalPrice.

**Orthogonality:** Basic price calculation remains independent of order lifecycle transitions.