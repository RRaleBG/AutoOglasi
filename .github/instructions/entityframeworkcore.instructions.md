---
description: 'EF Core guidance for AutoOglasi data and service layers.'
applyTo: 'Data/**/*.cs,Services/**/*.cs,Web/**/*.cs'
---

# Entity Framework Core Instructions

- Prefer EF Core async APIs such as `ToListAsync`, `CountAsync`, `AnyAsync`, `FirstOrDefaultAsync`, and `SaveChangesAsync` for database-backed operations.
- Keep query projection inside the database query where possible.
- Avoid loading extra data before filtering unless the logic cannot be translated to SQL.
- Keep `DbContext` usage inside services and startup initialization paths; do not move it into views.
- Keep migration and seeding behavior explicit and observable during startup.
- When adding or updating queries, preserve current business filters such as `!IsDeleted` and `IsPublic`.
- Preserve the current soft-delete strategy for `Car` and `Post` instead of replacing it with hard deletes.
- Keep important model constraints in `AutoOglasiDbContext`, including restricted delete behaviors and `decimal(18,2)` price mapping.
- When changing queries that touch related car, post, extra, image, or identity data, keep the current relationships and projections intact.
