---
description: 'AutoMapper profile guidance for DTO and view-model mappings in AutoOglasi.'
applyTo: 'Infrastructure/AutoOglasi.MapperConfigurations/Profiles/**/*.cs'
---

# AutoMapper Profile Instructions

- Keep mapping definitions in the dedicated profile classes under `Infrastructure/AutoOglasi.MapperConfigurations/Profiles`.
- Prefer extending an existing profile for the same domain before creating another profile.
- Map between entities, service DTOs, and web view models according to the existing layering rules; do not blur service and UI boundaries.
- Keep profile changes minimal and domain-focused so controller and service code can continue relying on centralized mappings.
- When adding new public mappings used by controllers, services, or tests, update or add tests that exercise the mapped behavior.
