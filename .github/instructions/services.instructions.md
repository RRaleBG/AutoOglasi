---
description: 'Service-layer guidance for business logic in AutoOglasi.'
applyTo: 'Services/**/*.cs'
---

# Services Instructions

- Keep business logic, validation, query composition, and persistence rules in the service layer.
- Use service DTOs from `Services/*/Models` as service contracts instead of exposing EF entities to the web layer.
- Prefer async EF Core and I/O operations end-to-end.
- Keep image, car, post, and statistics rules consistent with existing behavior, including soft-delete handling, post visibility, image-count limits, and cover-image logic.
- Reuse existing services and helper methods before adding new service types or abstractions.
- Keep queries readable and preserve repository business filters such as `!IsDeleted` and `IsPublic`.
- Add or update xUnit tests for changed public service behavior.
