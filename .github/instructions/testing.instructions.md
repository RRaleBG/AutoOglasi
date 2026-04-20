---
description: 'Unit testing guidance for new AutoOglasi test projects.'
applyTo: 'Tests/**/*.cs'
---

# Testing Instructions

- Prefer fast unit tests that do not require a running web server or real database unless integration behavior is the explicit goal.
- Use descriptive test names that state the expected behavior.
- Test pure logic and small service helpers first.
- Keep test dependencies minimal; do not add heavy mocking libraries unless there is a clear need.
- Arrange test data explicitly inside each test or a small local helper.
- Ensure tests can run with `dotnet test` from the solution root.
- Use xUnit in `Tests/AutoOglasi.Services.Tests` for new tests unless the solution gains a real need for another test project.
- Reuse the existing helpers such as `TestDbContextFactory`, `TestMapperFactory`, `TestIdentityManagers`, and `TestTempDataProvider` before creating new test infrastructure.
- Prefer EF Core InMemory, lightweight stubs, and public API tests over mocking repository code that belongs to this solution.
- When changing controller, page model, or service behavior, add or update tests for the affected public paths.
