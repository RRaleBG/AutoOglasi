---
description: 'Razor view and layout guidance for AutoOglasi UI files.'
applyTo: 'Web/**/*.cshtml'
---

# Razor UI Instructions

- Prefer Razor Pages and Razor view patterns already used in the repository over introducing new MVC or Blazor-specific patterns.
- Keep layout assets deduplicated; use one local or one CDN source per library, not both.
- Place page-specific scripts in `@section Scripts` where practical.
- Keep markup accessible and preserve existing route helpers such as `asp-controller`, `asp-action`, and `asp-route-*`.
- Avoid invalid HTML tags for non-HTML assets.
- Keep theme-toggle and validation scripts lightweight and resilient to missing DOM elements.
- Keep paging, sorting, admin actions, and image/post navigation aligned with the existing helper and partial patterns.
- Prefer editing existing views and partials over introducing new page patterns for features that already fit the current MVC and Razor structure.
