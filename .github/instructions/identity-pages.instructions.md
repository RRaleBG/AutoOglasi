---
description: 'Identity Razor Pages guidance for authentication and account management in AutoOglasi.'
applyTo: 'Web/**/Areas/Identity/**/*.cs,Web/**/Areas/Identity/**/*.cshtml'
---

# Identity Pages Instructions

- Keep the Identity area implemented with Razor Pages; do not convert account flows to MVC controllers or another UI stack unless asked.
- Preserve ASP.NET Core Identity conventions, model binding, and result types such as `Page`, `RedirectToPage`, and `LocalRedirect`.
- Keep authentication flows configuration-driven and compatible with the optional Google and GitHub providers configured in `Program.cs`.
- Never hardcode secrets, provider credentials, or connection strings.
- Keep return URL handling local and safe; do not introduce open redirect behavior.
- Preserve the current logging-first troubleshooting style used in account flows where it improves observability without leaking secrets.
- When modifying login, registration, logout, external login, or role-sensitive account behavior, add or update tests where practical.
