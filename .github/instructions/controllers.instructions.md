---
description: 'Controller guidance for MVC and Admin controllers in AutoOglasi.'
applyTo: 'Web/**/Controllers/**/*.cs'
---

# Controller Instructions

- Keep controllers thin and orchestration-focused: validate inputs, call services, map DTOs to view models, and return explicit MVC results.
- Do not query `AutoOglasiDbContext` directly from controllers.
- Preserve existing authorization and routing behavior, especially for authenticated post management and the Admin area.
- Prefer `NotFound`, `Unauthorized`, `View`, `RedirectToAction`, and similar explicit results over implicit fallthrough behavior.
- Reuse the existing AutoMapper profiles and view models before introducing new controller-specific mapping logic.
- Keep paging, sorting, TempData success/error messaging, and area redirects consistent with the existing controller patterns.
- When changing controller behavior, add or update xUnit tests for the affected action paths.
