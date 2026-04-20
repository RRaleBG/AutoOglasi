---
description: 'Documentation guidance for safe setup examples and sanitized repository docs.'
applyTo: 'README.md,.github/**/*.md,**/*.md'
---

# Documentation Safety Instructions

- Use placeholder values in configuration samples, authentication examples, and connection strings.
- Never copy secrets, credentials, tokens, or personal data from local files, prompts, screenshots, or logs into repository documentation.
- When application changes affect setup or security-sensitive behavior, update the README and related markdown files in the same task.
- Document required configuration keys by name and purpose instead of committing live values.
- Keep examples aligned with this repository's `.NET 10`, SQL Server, ASP.NET Core MVC, and Identity Razor Pages stack.
