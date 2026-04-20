# Copilot Instructions

## Repository Overview
- AutoOglasi is a layered car marketplace application built on `.NET 10` with ASP.NET Core MVC, ASP.NET Core Identity, Razor Pages for the Identity area, EF Core, AutoMapper, Bootstrap, and jQuery.
- Keep the repository on `.NET 10`; do not upgrade to `.NET 11`, change the SDK, or introduce a newer target framework unless the user explicitly asks.
- This file provides repository-wide guidance. Also follow the path-specific files in `.github/instructions/` when they apply, especially the C#, ASP.NET Core, EF Core, testing, controller, Identity Pages, service-layer, AutoMapper, Razor UI, and frontend asset instructions.

## Solution Structure
- Keep the current layering intact:
  - `Data/` for `DbContext`, entities, migrations, and seeding.
  - `Infrastructure/` for shared constants, validation attributes, and AutoMapper profiles.
  - `Services/` for business logic, EF Core queries, and image/file workflows.
  - `Web/` for controllers, Razor Pages, Razor views, web constants, and web view models.
  - `Tests/` for xUnit coverage.
- Do not move EF Core query logic into controllers, views, or Razor Pages.
- Do not introduce extra abstraction layers unless they support an actual dependency boundary or testing need.

## Architecture and Mapping Rules
- Keep controllers and Identity PageModels thin. They should coordinate services, map DTOs to view models, and return explicit MVC results.
- Put business rules in the service layer, especially post visibility, soft-delete behavior, search filtering, image handling, and startup database logic.
- Use service DTOs from `Services/*/Models` for service contracts and `Web/AutoOglasi.Web.ViewModels` for UI models. Do not bind EF entities directly to the UI.
- Keep AutoMapper profile changes in `Infrastructure/AutoOglasi.MapperConfigurations/Profiles/` instead of scattering manual mapping logic.
- Reuse existing constants from `AutoOglasi.GlobalConstants` and `AutoOglasi.Web.Constants` where the repository already has a named constant.

## Web Application Rules
- Preserve minimal hosting in `Web/AutoOglasi.Web/Program.cs`.
- Preserve the current middleware and startup behavior unless the change requires it:
  - `AutoValidateAntiforgeryTokenAttribute`
  - Identity with roles
  - cookie policy and TempData cookie configuration
  - status code redirect and exception handling behavior
  - startup migrations and seeding through `InitializeDatabaseAsync`
- Keep authentication providers configuration-driven. Google and GitHub auth are optional and should remain enabled only when configuration values are present.
- Never hardcode secrets, client IDs, connection strings, or auth credentials. Use configuration or user secrets.
- Keep checked-in `appsettings*.json` files sample-safe. Use placeholders in repository files and keep local secrets in user secrets, environment variables, or untracked development-only configuration.
- Keep the existing MVC plus Razor Pages approach; do not introduce Blazor, minimal API rewrites, or SPA frameworks unless asked.
- Preserve the Admin area routing and authorization model.

## Documentation and Security Hygiene
- Keep README and other markdown examples sanitized; never copy live secrets, tokens, connection strings, or personal data from local config, prompts, or logs into repository documentation.
- When setup, configuration, or security-sensitive behavior changes, update the relevant documentation in the same change.

## Data and EF Core Rules
- Prefer EF Core async APIs and keep async flows end-to-end.
- Preserve current business filters such as `!IsDeleted` and `IsPublic` when modifying post and car queries.
- Follow the existing soft-delete pattern for cars and posts instead of hard deletes unless the user explicitly asks otherwise.
- Keep relationship configuration and important persistence rules in `AutoOglasiDbContext`, including restricted deletes and decimal price mapping.
- When changing image or car update flows, preserve the current repository rules:
  - at least one image is required for a post
  - maximum of 10 images per car/post
  - cover image handling must remain consistent

## UI and Razor Rules
- Keep Razor views and Razor Pages aligned with the existing Bootstrap and jQuery approach.
- Prefer existing tag helpers (`asp-controller`, `asp-action`, `asp-route-*`) and current paging/sorting patterns.
- Keep page-specific scripts in `@section Scripts` when practical.
- Avoid introducing duplicate asset sources or replacing the existing UI stack without a request.

## Packages and Build Rules
- This repository uses Central Package Management via `Directory.Packages.props`. Add or update package versions there, not in individual project files, unless there is a specific exception already established.
- Respect analyzer and build strictness already in place, including `TreatWarningAsErrors` in the web project.
- Prefer package modernization within `.NET 10` compatibility instead of broader framework upgrades.

## Testing Rules
- Use xUnit for new tests and keep them in `Tests/AutoOglasi.Services.Tests` unless the solution grows a clear need for additional test projects.
- Prefer fast unit tests with EF Core InMemory, lightweight stubs, and repository test helpers over heavy mocking frameworks.
- Reuse and extend the existing test helpers when possible, such as:
  - `TestDbContextFactory`
  - `TestMapperFactory`
  - `TestIdentityManagers`
  - `TestTempDataProvider`
- Add or update tests for changed public behavior in controllers, services, and page models.
- After meaningful changes, run a solution build and the relevant xUnit tests.

## Modernization Guidance
- Prioritize package modernization on `.NET 10` and code modernization for the MVC and Razor Pages application instead of upgrading to `.NET 11`.
