---
description: 'Frontend asset and static file guidance for AutoOglasi.'
applyTo: 'Web/**/*.cshtml,Web/**/*.js,Web/**/*.css'
---

# Frontend Assets Instructions

- Prefer one source of truth for each frontend library.
- Keep static assets under `wwwroot` organized and reference minified local assets for production-style usage.
- Remove duplicate script or stylesheet references instead of layering multiple copies.
- Keep jQuery-dependent code wrapped in DOM-ready handlers only when needed.
- Avoid invalid asset references such as `<link>` tags pointing to JavaScript files.
- Preserve client-side validation and carousel behavior when changing shared layouts.
- Keep the current Bootstrap and jQuery stack; do not introduce SPA tooling or alternate UI frameworks unless asked.
- When changing scripts used by posts, search, or homepage statistics, preserve current selectors and progressive enhancement behavior.
