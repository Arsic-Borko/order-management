# Order Management — Development Roadmap

## 1. Purpose

This roadmap outlines the incremental development of Order Management as a technical foundation for a potential future product.

Development combines three objectives:

- Building a functional and maintainable application.
- Developing software architecture and algorithmic engineering skills.
- Exploring and validating potential real-world product opportunities.

The roadmap is intentionally flexible. Milestones represent learning and delivery goals rather than fixed deadlines.

## 2. Current Progress

### Phase 1 — Domain Foundation

**Status: Completed**

- [x] Create the .NET solution and project structure.
- [x] Implement the `Order` domain model.
- [x] Implement the `OrderItem` domain model.
- [x] Define order lifecycle states.
- [x] Enforce domain invariants and valid state transitions.
- [x] Calculate order totals from order items.
- [x] Introduce automated domain tests.
- [x] Document significant domain decisions using ADRs.

**Engineering focus:** Encapsulation, domain modeling, invariants, Design by Contract, and testability.

### Phase 2 — Application and API Foundation

**Status: In progress**

Completed:

- [x] Introduce the Application layer.
- [x] Define the repository abstraction.
- [x] Implement the CreateOrder use case.
- [x] Add application-level unit tests.

Next steps:

- [ ] Introduce the Infrastructure project.
- [ ] Implement `InMemoryOrderRepository`.
- [ ] Register application and infrastructure dependencies.
- [ ] Expose `POST /orders`.
- [ ] Introduce API integration tests.
- [ ] Implement order retrieval by identifier.

**Engineering focus:** Dependency inversion, separation of concerns, application orchestration, HTTP semantics, and integration testing.

**Algorithms and performance:**

- Evaluate `List<T>` and `Dictionary<TKey, TValue>` for in-memory storage.
- Analyze expected lookup, insertion, and deletion complexity.
- Consider memory usage and access patterns.
- Benchmark alternative implementations if the comparison provides meaningful insight.

### Phase 3 — Order Lifecycle and Persistence

**Status: Planned**

Potential activities:

- Add order items through application use cases.
- Confirm and cancel orders through the API.
- Introduce persistent storage.
- Implement transactional consistency where necessary.
- Define consistent API error responses.
- Expand integration testing.

**Engineering focus:** Persistence boundaries, transactional consistency, concurrency, domain errors, and API design.

**Algorithms and performance:**

- Analyze query complexity and indexing.
- Compare in-memory operations with database-backed operations.
- Investigate pagination and efficient filtering.
- Profile relevant application bottlenecks.

### Phase 4 — Operational Workflows

**Status: Exploratory**

Potential capabilities:

- Order prioritization.
- Scheduling and resource allocation.
- Operational status tracking.
- Assignment of work to available resources.
- Basic operational reporting.

These capabilities are not committed requirements. Their relevance depends on product research and future decisions.

**Engineering focus:** Domain evolution, extensibility, concurrency, and architectural trade-offs.

**Algorithms and performance:**

- Priority queues and heaps.
- Sorting and scheduling strategies.
- Greedy algorithms.
- Complexity analysis of resource allocation.

### Phase 5 — Intelligent Optimization

**Status: Exploratory**

Potential capabilities:

- Route planning.
- Delivery assignment optimization.
- Multi-stop scheduling.
- Resource utilization optimization.
- AI-assisted operational decision-making.

**Algorithms and performance:**

- Graph representations and traversal.
- Shortest-path algorithms, including Dijkstra and A*.
- Traveling Salesperson Problem (TSP).
- Vehicle Routing Problem (VRP).
- Heuristics and approximation algorithms.
- Performance benchmarking against baseline solutions.

The scope of this phase depends on identifying a real problem where optimization creates measurable value.

## 3. Continuous Engineering Practices

The following practices apply throughout development:

- Write automated tests for meaningful behavior.
- Maintain explicit domain invariants.
- Prefer simple designs over premature abstractions.
- Analyze algorithmic complexity when selecting data structures.
- Measure performance before introducing optimizations.
- Refactor when new requirements reveal weaknesses in the design.
- Record significant architectural decisions.
- Keep documentation aligned with implementation.

## 4. Continuous Learning

The project serves as a practical environment for developing deeper engineering knowledge.

### Software Architecture

Topics include:

- Domain-driven design fundamentals.
- Modular architecture.
- Dependency management.
- Architectural trade-offs.
- Reliability and consistency.
- Evolution toward more complex systems when justified.

### Algorithms and Data Structures

Use *Cracking the Coding Interview (6th Edition)* as a reference when relevant problems arise.

Prioritize practical application of algorithmic concepts within the project rather than maintaining a separate mandatory exercise schedule.

### Performance Engineering

Explore:

- Big O time and space complexity.
- Collection and data structure selection.
- BenchmarkDotNet.
- Memory allocation and garbage collection.
- Database query performance.
- Profiling and performance regression analysis.

## 5. Product Discovery

Product research proceeds alongside technical development.

Activities may include:

- Identifying operational problems in logistics and related industries.
- Evaluating existing solutions and competitors.
- Exploring underserved customer segments.
- Formulating testable product hypotheses.
- Validating assumptions through real-world feedback.

No customer segment, commercial model, or product-market fit has been established.

Product discoveries may change the roadmap.

## 6. Roadmap Principles

1. Deliver small, working increments.
2. Do not introduce complexity without a clear reason.
3. Prefer measurable engineering improvements.
4. Integrate learning into real implementation work.
5. Keep exploratory features separate from committed requirements.
6. Allow validated product needs to influence technical priorities.

## 7. Immediate Next Steps

1. Complete the initial project documentation.
2. Implement `InMemoryOrderRepository`.
3. Analyze repository data structure choices and their complexity.
4. Expose the CreateOrder use case through the API.
5. Add integration tests for the complete request flow.

The next development milestone is a working HTTP request that creates an order through the Application layer and stores it using the in-memory repository.