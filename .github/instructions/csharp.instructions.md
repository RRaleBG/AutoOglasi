---
description: 'C# coding guidance for the AutoOglasi solution on .NET 10.'
applyTo: '**/*.cs'
---

# C# Instructions

- Target `.NET 10` and prefer modern C# syntax that keeps the code clear.
- Preserve the existing coding style of the file or folder instead of reformatting unrelated code.
- Use `async` and `await` for EF Core and I/O-bound work.
- Avoid `Task.ContinueWith`; prefer direct `await` with explicit return values.
- Keep constructor injection explicit and use `private readonly` fields.
- Prefer guard clauses for invalid input and null results.
- Prefer precise exception types for new technical failures; keep existing user-facing business-rule messaging consistent where the repository already relies on it.
- Keep methods small and focused; extract shared controller logic into private helpers when it removes duplication.
- Use collection projections instead of manual loops when LINQ remains readable.
- Do not introduce new abstractions, wrappers, or interfaces unless they help isolate an external dependency or materially improve testing.
- Reuse existing DTOs, view models, constants, and helper methods before introducing new types.
- Keep nullable behavior explicit and avoid null-forgiving operators unless there is no better option.
- Do not introduce framework upgrades beyond `.NET 10` for this repository.
