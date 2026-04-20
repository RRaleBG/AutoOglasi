# .NET 10 modernization plan for `AutoOglasi`

## Table of Contents
- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Risk Management](#risk-management)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

## Executive Summary
This plan covers modernization of the `AutoOglasi` solution while keeping all projects on `net10.0`. The assessment confirms that all `10` projects already target `.NET 10`, all `25` detected packages are framework-compatible, and no API migration blockers were reported. The modernization scope is therefore focused on package rationalization, removal of legacy or misplaced references, hosting model updates, asynchronous data-access improvements, and validation hardening rather than target framework changes.

### Selected Strategy
**All-At-Once Strategy** — all projects are modernized in a single coordinated operation with no intermediate target-framework states.

**Rationale**:
- `10` projects in a homogeneous `.NET 10` solution
- clear dependency graph with one top-level web application
- no mandatory assessment issues and no framework-compatibility blockers
- package work is primarily cleanup and consolidation rather than broad version migration
- the main cross-cutting changes (`Program`/`Startup`, package placement, async data access, and frontend asset cleanup) benefit from coordinated execution

### Complexity Classification
**Medium** for planning purposes.

Justification:
- project count is moderate (`10`)
- dependency depth reaches Level `5`
- the data layer is the largest component (`AutoOglasi.Data` at `8479` LOC)
- no circular dependencies, no reported API breaks, and no assessment-stage package incompatibilities were found
- manual inspection identified architectural cleanup work that spans multiple layers, especially the web host, services, and package layout

### Iteration Strategy Used
A phase-based planning pass is used:
1. foundation sections and classification
2. dependency analysis and all-at-once strategy definition
3. project-by-project modernization details grouped by layer
4. risk, source control, and success criteria finalization

### Scope Highlights
- retain `net10.0` in all projects
- remove deprecated or legacy references such as `Microsoft.AspNetCore.Http.Features` and review `Microsoft.AspNetCore.Razor`
- remove misplaced packages from class-library projects where the package does not belong to the layer
- modernize web hosting from `Program` + `Startup` split to minimal hosting in `Program.cs`
- convert synchronous EF Core read paths to asynchronous service and controller flows
- clean duplicate or conflicting frontend asset loading in shared layout files
- centralize package version governance with `Directory.Packages.props`

## Migration Strategy
### Approach Selection
This plan uses an **All-At-Once Strategy**. All projects remain on `net10.0`, and modernization changes are applied as one coordinated solution-wide operation instead of project-by-project framework migration.

### Why All-At-Once Fits This Solution
- every project already targets the same framework
- the assessment found `0` framework incompatibilities and `0` API issues
- package work is mostly rationalization, legacy removal, and placement correction
- the largest architectural changes (`Startup` to minimal hosting, async service/controller flow, central package management) cross project boundaries and should land consistently
- a single validation cycle is practical because there is one top-level web application and no separate test-project tree to coordinate

### Atomic Upgrade Scope
The modernization operation should be treated as one bounded batch:
1. introduce central package management with `Directory.Packages.props`
2. remove or relocate misplaced package references across all class libraries
3. retain only valid web-specific packages in `Web\AutoOglasi.Web`
4. migrate web startup composition from `Startup`-based bootstrapping to minimal hosting in `Program.cs`
5. move database initialization logic out of request-host startup or isolate it behind an explicit initialization pattern
6. convert synchronous EF Core access patterns to async service and controller flows where reads are currently synchronous
7. clean duplicate frontend assets and legacy static references
8. restore dependencies, build the full solution, fix resulting compilation issues, and verify a clean build

### Parallel vs Sequential Decisions
**Sequential by dependency reasoning, atomic by execution.**

Within the single modernization branch, analysis and edits should follow dependency order from foundations to the web app. However, the resulting implementation is committed and validated as one coordinated set rather than as separate deployable phases.

### Implementation Timeline
#### Phase 0: Preparation
- create a dedicated modernization branch
- preserve pending local changes before edits begin
- establish `Directory.Packages.props` ownership rules
- capture current restore/build baseline for comparison

#### Phase 1: Atomic Modernization
**Operations performed in one coordinated batch:**
- normalize package ownership in all projects
- remove deprecated and legacy references
- update host composition and startup wiring
- update service and controller signatures for async flows
- clean layout asset references and package usage boundaries
- restore and build the entire solution
- fix compile-time issues introduced by package or host changes

**Deliverable:** full solution builds cleanly on `net10.0`

#### Phase 2: Validation
**Operations:**
- execute automated tests if a test project is added or already exists by execution time
- perform targeted application validation for authentication, post creation, search, edit, and latest-post rendering
- verify startup, migrations, and static assets behave correctly

**Deliverable:** modernization changes operate correctly with no dependency drift

### Package Governance Rules
- introduce `Directory.Packages.props` at repository root
- move shared versions there for all retained packages
- keep analyzer packages as development-only dependencies with `PrivateAssets=all`
- keep web-only packages out of low-level libraries
- remove packages from projects that do not consume their APIs

### Source-of-Truth for Compatibility
Use the assessment data for framework compatibility and the manual review findings for architectural cleanup priorities. Empty suggested package versions in the assessment should not be interpreted as a reason to keep unnecessary references.

## Detailed Dependency Analysis
### Dependency Graph Summary
The solution forms a clean layered graph with no circular dependencies.

**Level 0 — Foundation projects**
- `Infrastructure\AutoOglasi.CustomAttributes\AutoOglasi.CustomAttributes.csproj`
- `Data\AutoOglasi.Data.Common\AutoOglasi.Data.Common.csproj`
- `Infrastructure\AutoOglasi.GlobalConstants\AutoOglasi.GlobalConstants.csproj`
- `Web\AutoOglasi.Web.Constants\AutoOglasi.Web.Constants.csproj`

**Level 1 — Domain surface projects**
- `Data\AutoOglasi.Data.Models\AutoOglasi.Data.Models.csproj` depends on `AutoOglasi.Data.Common`
- `Web\AutoOglasi.Web.ViewModels\AutoOglasi.Web.ViewModels.csproj` depends on `AutoOglasi.CustomAttributes` and `AutoOglasi.Data.Common`

**Level 2 — Data access project**
- `Data\AutoOglasi.Data\AutoOglasi.Data.csproj` depends on `AutoOglasi.Data.Models` and `AutoOglasi.GlobalConstants`

**Level 3 — Service layer**
- `Services\AutoOglasi.Services\AutoOglasi.Services.csproj` depends on `AutoOglasi.Data.Models` and `AutoOglasi.Data`

**Level 4 — Mapping layer**
- `Infrastructure\AutoOglasi.MapperConfigurations\AutoOglasi.MapperConfigurations.csproj` depends on `AutoOglasi.Data.Models`, `AutoOglasi.Services`, and `AutoOglasi.Web.ViewModels`

**Level 5 — Root application**
- `Web\AutoOglasi.Web\AutoOglasi.Web.csproj` depends on `AutoOglasi.Data.Models`, `AutoOglasi.MapperConfigurations`, `AutoOglasi.Web.Constants`, `AutoOglasi.Services`, `AutoOglasi.GlobalConstants`, and `AutoOglasi.Web.ViewModels`

### Critical Path
The longest dependency chain is:
`AutoOglasi.Data.Common` → `AutoOglasi.Data.Models` → `AutoOglasi.Data` → `AutoOglasi.Services` → `AutoOglasi.MapperConfigurations` → `AutoOglasi.Web`

This path drives the order in which modernization changes should be reasoned about even though execution remains atomic.

### Modernization Groupings
Because the selected execution strategy is all-at-once, these groupings describe analysis and verification focus rather than separate implementation waves.

**Group A — Foundation cleanup**
- `AutoOglasi.CustomAttributes`
- `AutoOglasi.Data.Common`
- `AutoOglasi.GlobalConstants`
- `AutoOglasi.Web.Constants`

Primary concerns:
- remove packages that do not belong in low-level libraries
- keep only true shared abstractions and constants

**Group B — Contract and DTO boundary**
- `AutoOglasi.Data.Models`
- `AutoOglasi.Web.ViewModels`

Primary concerns:
- remove legacy ASP.NET references from non-web DTO/model assemblies
- keep web-only types such as `IFormFile` isolated to view-model/UI-facing assemblies

**Group C — Runtime and data flow**
- `AutoOglasi.Data`
- `AutoOglasi.Services`
- `AutoOglasi.MapperConfigurations`
- `AutoOglasi.Web`

Primary concerns:
- convert synchronous query paths to async
- modernize hosting and startup initialization
- centralize package versions and clean web asset loading

### Ordering Principles
Even in a single atomic change set, execution should respect this reasoning order:
1. normalize shared package ownership in foundation projects
2. clean DTO/model and view-model package references
3. update data and service layers for async and package cleanup
4. migrate hosting and startup composition in the web app
5. restore, build, fix compile-time issues, and validate the full solution

### Circular Dependencies
No circular dependencies were reported by the assessment. This lowers structural risk for a single coordinated modernization pass.

## Project-by-Project Plans
### Project: `Data\AutoOglasi.Data.Common\AutoOglasi.Data.Common.csproj`
**Current State**: `net10.0`, foundation class library, no project dependencies, depended on by `AutoOglasi.Data.Models` and `AutoOglasi.Web.ViewModels`.

**Target State**: remain on `net10.0` with only true shared abstractions or constants retained.

**Migration Focus**:
1. remove non-foundation package references that are not used by the assembly
2. keep the project lightweight and free of web/runtime concerns
3. validate that dependants still compile after package pruning

### Project: `Data\AutoOglasi.Data.Models\AutoOglasi.Data.Models.csproj`
**Current State**: `net10.0`, domain model library, depends on `AutoOglasi.Data.Common`, consumed by data, services, mapper configuration, and web layers.

**Target State**: remain on `net10.0` with domain entities isolated from obsolete or web-centric package references.

**Migration Focus**:
1. review and remove the `Microsoft.AspNetCore.Razor` `2.3.0` reference unless a concrete compile-time need is proven
2. remove unrelated package references inherited from copy/paste package patterns
3. confirm entity and identity types remain stable for downstream consumers

### Project: `Data\AutoOglasi.Data\AutoOglasi.Data.csproj`
**Current State**: `net10.0`, EF Core data-access layer, largest codebase component, depends on `AutoOglasi.Data.Models` and `AutoOglasi.GlobalConstants`.

**Target State**: remain on `net10.0` with focused EF Core package ownership and predictable initialization responsibilities.

**Migration Focus**:
1. retain only packages required for EF Core runtime and design-time tooling
2. ensure database startup responsibilities are not duplicated between data layer and web host
3. validate migrations, design-time context behavior, and package boundaries

### Project: `Infrastructure\AutoOglasi.CustomAttributes\AutoOglasi.CustomAttributes.csproj`
**Current State**: `net10.0`, small attribute library, used by `AutoOglasi.Web.ViewModels`.

**Target State**: remain on `net10.0` with only attribute-related dependencies.

**Migration Focus**:
1. remove all package references that are not directly used by custom validation attributes
2. keep the project free of web host, OAuth, and NuGet-client concerns
3. verify downstream model-validation compilation remains intact

### Project: `Infrastructure\AutoOglasi.GlobalConstants\AutoOglasi.GlobalConstants.csproj`
**Current State**: `net10.0`, constants-only library, used by `AutoOglasi.Data` and `AutoOglasi.Web`.

**Target State**: remain on `net10.0` with no unnecessary external packages.

**Migration Focus**:
1. remove all package references unless actual source usage requires them
2. keep the project as a pure constants/shared-values assembly
3. ensure consuming projects compile without indirect package leakage

### Project: `Infrastructure\AutoOglasi.MapperConfigurations\AutoOglasi.MapperConfigurations.csproj`
**Current State**: `net10.0`, mapping configuration layer between domain/services/view models, depends on `AutoOglasi.Data.Models`, `AutoOglasi.Services`, and `AutoOglasi.Web.ViewModels`.

**Target State**: remain on `net10.0` with mapping-only dependencies.

**Migration Focus**:
1. keep `AutoMapper` if required and remove unrelated packages
2. validate profile discovery after hosting modernization
3. ensure mapping contracts still align with async service return types

### Project: `Services\AutoOglasi.Services\AutoOglasi.Services.csproj`
**Current State**: `net10.0`, service layer depending on `AutoOglasi.Data` and `AutoOglasi.Data.Models`, consumed by mapper configurations and web app.

**Target State**: remain on `net10.0` with async-first data access and only service-relevant dependencies.

**Migration Focus**:
1. remove deprecated `Microsoft.AspNetCore.Http.Features` unless required by actual service APIs
2. remove unrelated UI, OAuth, or NuGet-client packages that are unused in services
3. convert synchronous query methods to async variants where they access EF Core
4. update calling web controllers to await service methods

### Project: `Web\AutoOglasi.Web.Constants\AutoOglasi.Web.Constants.csproj`
**Current State**: `net10.0`, small constants library consumed by the web app.

**Target State**: remain on `net10.0` with no unnecessary external packages.

**Migration Focus**:
1. remove packages that are not directly used by constants-only code
2. keep this project isolated from runtime and UI package concerns
3. verify the web app compiles after cleanup

### Project: `Web\AutoOglasi.Web.ViewModels\AutoOglasi.Web.ViewModels.csproj`
**Current State**: `net10.0`, web-facing view-model assembly, depends on `AutoOglasi.CustomAttributes` and `AutoOglasi.Data.Common`, consumed by mapper configurations and web app.

**Target State**: remain on `net10.0` with only UI-facing model dependencies retained.

**Migration Focus**:
1. keep packages that are required for view-model concerns such as `IFormFile`
2. remove deprecated `Microsoft.AspNetCore.Http.Features` if it is unnecessary after API review or replace with a current abstraction if still needed
3. remove unrelated packages that should belong only to the web host
4. confirm model binding and validation still work after cleanup

### Project: `Web\AutoOglasi.Web\AutoOglasi.Web.csproj`
**Current State**: `net10.0`, top-level ASP.NET Core application, depends on six internal projects, contains legacy `Program` + `Startup` split, runtime compilation, authentication providers, EF startup initialization, and duplicated frontend asset loading.

**Target State**: remain on `net10.0` with minimal hosting, web-only package ownership, cleaned asset references, and async controller/service flows.

**Migration Focus**:
1. migrate `Program.cs` and `Startup.cs` composition to minimal hosting in `Program.cs`
2. gate `RazorRuntimeCompilation` to development-only usage if retained
3. move blocking migration/seeding logic out of request-host startup or encapsulate it in an explicit initialization path
4. remove `Shouldly` from the web app and relocate it to a dedicated test project if tests are added
5. keep only true web dependencies in this project and eliminate duplicate CSS/JS asset loading
6. update controllers to await async service methods and reduce repeated mapping boilerplate where practical

## Package Update Reference
### Common Package Actions
| Package | Current | Target | Projects Affected | Planned Action | Reason |
| :--- | :---: | :---: | :--- | :--- | :--- |
| `AspNet.Security.OAuth.GitHub` | `10.0.0` | `10.0.0` in `Directory.Packages.props` | all projects currently reference it | retain only in `Web\AutoOglasi.Web`; remove from class libraries | GitHub authentication is configured in web startup and should not leak into non-web projects |
| `AutoMapper` | `16.1.1` | `16.1.1` in `Directory.Packages.props` | all projects currently reference it | retain in `Infrastructure\AutoOglasi.MapperConfigurations`, `Services\AutoOglasi.Services`, and `Web\AutoOglasi.Web`; remove from foundations/constants libraries and review `Data\AutoOglasi.Data.Models` plus `Web\AutoOglasi.Web.ViewModels` for direct source usage before keeping | mapping concerns should stay in mapping, service, and web layers only |
| `jQuery` | `3.7.1` | `3.7.1` in `Directory.Packages.props` only if still managed through NuGet static assets | all projects currently reference it | retain only where static web assets are actually served; remove from all class libraries | JavaScript client assets do not belong in domain, infrastructure, or service assemblies |
| `NuGet.Packaging` | `7.3.1` | remove unless concrete source usage is found | all projects currently reference it | remove from all projects that do not import its namespaces | no active product-code usage was identified during manual inspection |
| `NuGet.Protocol` | `7.3.1` | remove unless concrete source usage is found | all projects currently reference it | remove from all projects that do not import its namespaces | no active product-code usage was identified during manual inspection |
| `System.Security.Cryptography.Xml` | `10.0.6` | keep only where cryptographic XML APIs are actually consumed | most class libraries and view-model project | verify source usage and prune aggressively | shared blanket reference pattern suggests over-inclusion |

### Deprecation and Legacy Cleanup
| Package | Current | Target | Projects Affected | Planned Action | Reason |
| :--- | :---: | :---: | :--- | :--- | :--- |
| `Microsoft.AspNetCore.Http.Features` | `5.0.17` | remove or replace after validation | `Services\AutoOglasi.Services`, `Web\AutoOglasi.Web.ViewModels` | remove from `Services`; in `Web.ViewModels` replace only if a current abstraction is required for `IFormFile`, otherwise remove | package is deprecated/legacy and does not belong in service layer |
| `Microsoft.AspNetCore.Razor` | `2.3.0` | remove | `Data\AutoOglasi.Data.Models` | remove unless a direct compile-time dependency is proven during execution | legacy web package in a data-model assembly is architecturally mismatched |
| `Shouldly` | `4.3.0` | move to dedicated test project | `Web\AutoOglasi.Web` | remove from web app; add to a future test project only if assertions are needed | assertion library should not ship with the application host |

### Web-Only Retained Packages
| Package | Current | Target | Projects Affected | Planned Action |
| :--- | :---: | :---: | :--- | :--- |
| `Microsoft.AspNetCore.Authentication.Facebook` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep and centralize version |
| `Microsoft.AspNetCore.Authentication.Google` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep and centralize version |
| `Microsoft.AspNetCore.Authentication.MicrosoftAccount` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep and centralize version |
| `Microsoft.AspNetCore.Identity.UI` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep and centralize version |
| `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep |
| `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | keep only for development-time configuration if still needed |
| `OwlCarousel` | `1.3.3` | `1.3.3` | `Web\AutoOglasi.Web` | keep only if the layout still serves these assets after duplicate cleanup |

### Data and Tooling Packages
| Package | Current | Target | Projects Affected | Planned Action |
| :--- | :---: | :---: | :--- | :--- |
| `Microsoft.EntityFrameworkCore.SqlServer` | `10.0.6` | `10.0.6` | `Data\AutoOglasi.Data`, `Web\AutoOglasi.Web` | keep where runtime SQL Server access is required |
| `Microsoft.EntityFrameworkCore.Sqlite` | `10.0.6` | `10.0.6` | `Web\AutoOglasi.Web` | verify actual use; remove if no runtime or testing need remains |
| `Microsoft.EntityFrameworkCore.Design` | `10.0.6` | `10.0.6` | `Data\AutoOglasi.Data` | keep as design-time dependency |
| `Microsoft.EntityFrameworkCore.Tools` | `10.0.6` | `10.0.6` | `Data\AutoOglasi.Data`, `Web\AutoOglasi.Web` | keep with `PrivateAssets=all` only where tooling is actually required |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | `10.0.2` | `10.0.2` | `Web\AutoOglasi.Web` | keep only if scaffolding remains part of the development workflow |
| `StyleCop.Analyzers` | `1.2.0-beta.556` | `1.2.0-beta.556` | `Web\AutoOglasi.Web` | keep as analyzer-only dependency |
| `SonarAnalyzer.CSharp` | `10.23.0.137933` | `10.23.0.137933` | `Web\AutoOglasi.Web` | keep as analyzer-only dependency |

## Breaking Changes Catalog
### Hosting and Startup Composition
- `Program.cs` currently uses `CreateHostBuilder(...).ConfigureWebHostDefaults(...).UseStartup<Startup>()`; moving to minimal hosting changes service-registration and middleware composition shape.
- `Startup.ConfigureServices` and `Startup.Configure` content must be merged into `Program.cs` without changing middleware order.
- Authentication and Razor Pages/MVC registration must preserve current behavior for identity UI and external providers.

### Database Initialization
- current startup performs `Database.Migrate()` plus seeding during application boot
- execution must decide whether initialization remains in-process, moves behind a hosted initializer, or moves to deployment-time commands
- error handling currently swallows startup-time migration faults into logging; behavior should remain intentional after modernization

### Async Data Access
- synchronous read methods in `PostsService`, `CarsService`, and `ImagesService` are expected to change to `async`/`await`
- controller actions such as `HomeController.Index`, `HomeController.About`, and multiple `PostsController` actions must change signatures when they await service calls
- AutoMapper projections and image-path assembly logic must remain valid when query materialization timing changes

### Package Cleanup
- removing widely copied package references may expose hidden compile-time dependencies
- replacing `Microsoft.AspNetCore.Http.Features` may require type import adjustments around `IFormFile`
- removing `Microsoft.AspNetCore.Razor` from `Data.Models` may reveal stale transitive assumptions

### Frontend Asset Cleanup
- `_Layout.cshtml` currently mixes CDN and local Bootstrap references and loads duplicate OwlCarousel scripts
- removing duplicates can change load order and plugin initialization timing
- validation scripts and theme-toggle code should be rechecked after asset normalization

## Testing & Validation Strategy
### Solution-Wide Validation Goals
- the solution restores successfully after package centralization
- the solution builds cleanly on `net10.0`
- no deprecated package reference remains without explicit justification
- all affected controller, service, and startup flows behave as before or better

### Automated Validation
Because no dedicated test project was discovered during assessment, execution should rely on:
1. solution restore validation
2. full solution build validation with warnings reviewed carefully
3. analyzer pass review after package cleanup and hosting migration
4. any existing CI checks already configured for the repository

### Recommended Test Project Follow-Up
Add a dedicated test project after or alongside execution if feasible. This is especially useful for:
- `PostsService`
- `CarsService`
- `ImagesService`
- route and startup smoke tests for the web host

### Functional Validation Checklist
**Authentication and Identity**
- external login providers still appear and challenge correctly
- identity UI pages still render and authenticate users correctly

**Posts and Cars Workflows**
- latest posts render on home and about pages
- search still filters and sorts correctly
- offer, create, edit, and mine flows still function correctly
- image upload and cover image selection still work

**Startup and Data**
- application startup completes without migration or seeding deadlocks
- database connectivity and EF Core migrations still behave as expected
- dependency injection resolves all services and AutoMapper profiles correctly

**Frontend**
- shared layout renders correctly after asset de-duplication
- carousel behavior still works on pages that use it
- client validation scripts still load once and validate forms correctly
- theme toggle still works after script cleanup

### Per-Project Validation Expectations
- foundation and constants projects: compile cleanly after package removal
- DTO/model and view-model projects: compile cleanly with no lost type references
- data and service projects: async signatures compile through all call chains
- web project: starts successfully and routes, authentication, and static assets behave correctly

## Source Control Strategy
### Branching
- use a dedicated modernization branch derived from `master`
- prefer a branch name that reflects the retained framework and modernization scope, for example `modernize-net10-packages-hosting`
- preserve pending local changes before branch creation
- if command-line Git is unavailable on the workstation, create the branch through the IDE or another Git client before execution begins

### Commit Strategy
**Preferred:** one atomic commit for the coordinated modernization after the solution restores, builds, and validates successfully.

If one commit is not practical, use at most these bounded commits:
1. `build: introduce central package management and prune misplaced references`
2. `refactor: migrate web host to minimal hosting and async service flows`
3. `chore: clean static assets and finalize validation adjustments`

### Review Expectations
- review package removals project by project against actual source usage
- review `Program.cs` middleware and service-registration order carefully
- review async signature changes across controllers and services as one unit
- block merge unless solution restore/build is clean and startup validation is complete

## Success Criteria
### Technical Criteria
- all projects still target `net10.0`
- `Directory.Packages.props` becomes the single version source for retained packages
- deprecated `Microsoft.AspNetCore.Http.Features` references are removed or explicitly replaced with a justified current alternative
- legacy `Microsoft.AspNetCore.Razor` reference is removed from `Data\AutoOglasi.Data.Models` unless proven necessary
- web-only packages are removed from non-web libraries
- `Program`/`Startup` composition is modernized to minimal hosting while preserving behavior
- synchronous EF Core read paths targeted by the plan are converted to async where appropriate
- the solution restores and builds cleanly after the modernization batch

### Quality Criteria
- package ownership aligns with architectural layers
- no duplicate Bootstrap or OwlCarousel references remain in shared layout composition
- analyzer packages remain development-only
- startup initialization is explicit, observable, and not dependent on blocking request-host boot logic

### Process Criteria
- execution follows the all-at-once strategy with one coordinated validation cycle
- dependency reasoning is respected from foundation projects through the web app even though execution is atomic
- source control uses a dedicated branch and preferably a single atomic modernization commit
- no placeholder or speculative package decisions remain unresolved at merge time
