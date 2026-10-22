# GitHub Copilot Instructions — ASP.NET Core Web API & EF Core

Write clean, concise, modern, production-ready C# for ASP.NET Core Web API and Entity Framework Core.

Favor correctness, security, readability, maintainability, and performance over brevity.

## Project conventions

- Inspect relevant existing code and project conventions before making changes.
- Preserve the established architecture, .NET and package versions, naming conventions, formatting, API contracts, and database behavior unless the requested task explicitly requires otherwise.
- Prefer the simplest maintainable solution that fits the existing architecture.
- Avoid unnecessary abstractions, dependencies, boilerplate, files, and unrelated refactoring.
- Keep responsibilities appropriately separated when doing so improves clarity or maintainability.
- Use existing shared utilities and patterns instead of introducing competing implementations.

## C# and ASP.NET Core

- Follow existing nullable reference type settings and use explicit, meaningful names.
- Do not add the `Async` suffix to method names, including asynchronous methods returning `Task`, `Task<T>`, or `ValueTask`. Apply this consistently to new methods in interfaces, implementations, services, repositories, and controllers. Preserve framework-required method names and existing public contracts when renaming would break compatibility.
- Prefer dependency injection and framework-provided features over custom infrastructure.
- Use asynchronous APIs for I/O-bound work and propagate `CancellationToken` where appropriate.
- Validate untrusted input at the API boundary; return consistent, appropriate HTTP status codes and error responses.
- Preserve request/response DTOs and public API compatibility unless a change is explicitly requested.
- Do not expose internal exceptions, secrets, or sensitive data in API responses or logs.
- Follow existing authentication and authorization policies; enforce authorization server-side.
- Avoid blocking on asynchronous operations, unnecessary allocations, and premature optimization.

## Entity Framework Core

- Follow the existing `DbContext`, entity configuration, repository/service, and transaction patterns.
- Prefer LINQ queries that EF Core can translate to SQL; avoid premature materialization and client-side filtering of large datasets.
- Use `AsNoTracking()` for read-only queries when tracking is unnecessary.
- Project only the fields needed for read endpoints; apply filtering, ordering, and pagination in the database.
- Avoid N+1 queries; use appropriate projections, includes, or explicit loading based on the use case.
- Never concatenate user input into raw SQL. Use LINQ or properly parameterized SQL.
- Preserve database schema, relationships, constraints, indexes, and migration history unless the task requires changes.
- Do not create or apply migrations automatically unless explicitly requested.
- Respect existing concurrency handling, transaction boundaries, and cancellation behavior.

## Quality and scope

- Handle expected failure cases explicitly; do not hide errors with broad catches or silent fallbacks.
- Add or update focused tests when behavior changes, following the project's testing conventions.
- Keep changes scoped to the request and avoid speculative features.
- Ensure proposed code compiles against the project's actual target framework and installed dependencies.
- If an important requirement is ambiguous, inspect nearby code first; make the smallest reasonable assumption and state it clearly.
- Briefly summarize material changes and any tests or checks performed; never claim tests passed unless they were run.
