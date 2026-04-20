---
description: 'Security guidance for AutoOglasi configuration, secrets, and auth-sensitive web changes.'
applyTo: 'Web/**/Program.cs,Web/**/appsettings*.json,Web/**/Controllers/**/*.cs,Web/**/Areas/Identity/**/*.cs,Web/**/Areas/Identity/**/*.cshtml'
---

# Configuration Security Instructions

- Never commit real secrets, client secrets, passwords, tokens, or production connection strings to tracked files.
- Keep checked-in `appsettings*.json` files sample-safe by using placeholders for connection strings and external provider credentials.
- Prefer user secrets, environment variables, or other untracked local configuration for development credentials.
- Preserve the current configuration-driven authentication pattern where Google and GitHub providers are enabled only when both values are present.
- Keep `AutoValidateAntiforgeryTokenAttribute`, cookie policy, HTTPS redirection, authentication, authorization, and HSTS behavior intact unless a task explicitly changes security requirements.
- Do not introduce open redirect behavior in controllers or Identity Razor Pages.
- Do not log secrets, access tokens, or full connection strings.
- When security-sensitive configuration keys change, update setup documentation in the same change.
