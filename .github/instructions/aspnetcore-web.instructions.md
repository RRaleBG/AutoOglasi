---
description: 'ASP.NET Core web application guidance for AutoOglasi.'
applyTo: 'Web/**/*.cs'
---

# ASP.NET Core Web Instructions

- Keep the application on `.NET 10` with minimal hosting in `Program.cs`.
- Preserve middleware ordering unless there is a clear reason to change it.
- Keep authentication, authorization, and routing behavior stable when modifying startup code.
- Prefer asynchronous controller actions when calling async service APIs.
- Return `NotFound`, `Unauthorized`, or redirects explicitly instead of relying on implicit behavior.
- Keep service registration and configuration in `Program.cs`; avoid reintroducing `Startup.cs`.
- Keep controllers thin: map models, call services, and return MVC results instead of embedding EF Core or business logic.
- Preserve the current external authentication pattern where Google and GitHub providers are enabled only when configuration values are present.
- Keep antiforgery, cookie policy, TempData cookie behavior, exception handling, and status code redirects aligned with the current startup pipeline.
- Preserve the Admin area and Identity area route structure when adding or changing endpoints.
- When adding endpoints, ensure unauthenticated admin routes continue to redirect or challenge correctly.
