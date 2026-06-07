
<p align="center" id="autooglasi">
  <img src="https://cdn-icons-png.flaticon.com/512/6295/6295417.png" width="100" />
</p>
<p align="center">
    <h1 align="center">AutoOglasi</h1>
</p>
<p align="center">
    <em><code> Car dealership web application </code></em>
</p>
<p align="center">
	<img src="https://img.shields.io/github/license/RRaleBG/AutoOglasi?style=flat&color=0080ff" alt="license">
	<img src="https://img.shields.io/github/last-commit/RRaleBG/AutoOglasi?style=flat&logo=git&logoColor=white&color=3280ff" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/RRaleBG/AutoOglasi?style=flat&color=0080ff" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/RRaleBG/AutoOglasi?style=flat&color=2382ff" alt="repo-language-count">
<p>
<p align="center">
		<em>Developed with the software and tools below.</em>
</p>
<p align="center">
   <img src="https://img.shields.io/badge/C%23-256860?style=flat&logo=c-sharp&logoColor=white"/>
    <img src="https://img.shields.io/badge/ASP.NET_Core-512BD4?style=flat&logo=dotnet&logoColor=white" alt="ASP.NET Core">
    <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=flat&logo=dotnet&logoColor=white" alt="EF Core">
    <img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white"/>
    <img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=flat&logo=JavaScript&logoColor=black" alt="JavaScript">
    <img src="https://img.shields.io/badge/xUnit-5E5E5E?style=flat&logo=dotnet&logoColor=white" alt="xUnit">
</p>
<hr>

## 🔗 Quick Links

> - [🚀 Getting Started](#-getting-started)
> - [📂 Repository Structure](#-repository-structure)
>   - [⚙️ Installation](#️-installation)
>   - [🧪 Tests](#-tests)
> - [🤝 Contributing](#-contributing)
> - [📄 License](#-license)
> - [👏 Acknowledgments](#-acknowledgments)

---

## 🚀 Getting Started

***Requirements***

Ensure you have the following dependencies installed on your system:

* **.NET SDK**: `10.0`
* **Database**: SQL Server-compatible connection for the web application

### 🏗️ Architecture at a Glance

AutoOglasi follows a layered architecture:

- `Data/` contains EF Core data access, entities, migrations, and seed logic.
- `Infrastructure/` contains shared constants, validation attributes, and AutoMapper profiles.
- `Services/` contains application business logic for cars, posts, images, and statistics.
- `Web/` contains ASP.NET Core MVC controllers, Identity Razor Pages, views, and UI models.
- `Tests/` contains xUnit tests with lightweight helpers and EF Core InMemory-based coverage.

### ⚙️ Installation

1. Clone the AutoOglasi repository:

```sh
git clone https://github.com/RRaleBG/AutoOglasi
```

2. Change to the project directory:

```sh
cd AutoOglasi
```

3. Restore and build the solution:

```sh
dotnet build
```

4. Configure the application settings for the web project.

- Keep the checked-in `Web/AutoOglasi.Web/appsettings.json` file sample-safe.
- Store local secrets in `appsettings.Development.json`, environment variables, or user secrets.
- Set a valid `ConnectionStrings:DefaultConnection` for your SQL Server instance.
- Optional external login providers are configuration-driven:
  - `Google:ClientId`
  - `Google:ClientSecret`
  - `GitHub:ClientId`
  - `GitHub:ClientSecret`

Use placeholder values in repository files and only supply real credentials in local, non-committed configuration.

The application applies EF Core migrations and seed data during startup.

### 🤖 Running AutoOglasi

Use the following command to run AutoOglasi:

```sh
dotnet run --project Web/AutoOglasi.Web
```

### 🧪 Tests:

```sh
dotnet test
```

## 🧭 Development Notes

- The repository targets `.NET 10` and uses Central Package Management via `Directory.Packages.props`.
- Package updates should stay compatible with `.NET 10` unless a wider upgrade is explicitly planned.
- Controllers and Razor Pages are intentionally thin; business rules and EF Core queries belong in the `Services/` layer.
- AutoMapper profiles live under `Infrastructure/AutoOglasi.MapperConfigurations/Profiles`.
- Cars and posts use soft-delete behavior, so new queries should preserve filters such as `!IsDeleted` and `IsPublic` where appropriate.
---


## 📂 Repository Structure

```sh
└── AutoOglasi/    
    ├── Data
    │   ├── AutoOglasi.Data
    │   ├── AutoOglasi.Data.Common
    │   └── AutoOglasi.Data.Models
    |
    ├── Infrastructure
    │   ├── AutoOglasi.CustomAttributes
    │   └── AutoOglasi.GlobalConstants
    | 
    ├── Services
    │   └── AutoOglasi.Services
    │       │   ├── IImagesService.cs
    │       │   ├── ImagesService.cs
    │       │   └── Models
    │       ├── Posts
    │       │   ├── IPostsService.cs
    │       │   ├── Models
    │       └── Statistics
    │           ├── IStatisticsService.cs
    │           └── Models
    └── Web
        ├── AutoOglasi.Web
        │   ├── Areas
        │   │   ├── Admin
        │   │   │   ├── AdminConstants.cs
        │   │   │   ├── Controllers
        │   │   │   └── Views
        │   │   └── Identity
        │   │       └── Pages
        │   ├── Controllers
        │   │   ├── HomeController.cs
        │   │   └── PostsController.cs
        │   ├── Views
        │   │   ├── Home
        │   │   │   ├── About.cshtml
        │   │   │   └── Index.cshtml
        │   │   ├── Posts
        │   │   │   ├── All.cshtml
        │   │   │   ├── Create.cshtml
        │   │   │   ├── Delete.cshtml
        │   │   │   ├── Edit.cshtml
        │   │   │   ├── Mine.cshtml
        │   │   │   ├── Offer.cshtml
        │   │   │   └── Search.cshtml
        │   │   ├── Shared
        │   │   │   ├── Error.cshtml
        │   │   │   ├── _Favicons.cshtml
        │   │   │   ├── _Layout.cshtml
        │   │   │   ├── _LoginPartial.cshtml
        │   │   │   ├── _PagingPartial.cshtml
        │   │   │   ├── _SortingPartial.cshtml
        │   │   │   └── _ValidationScriptsPartial.cshtml
        │   │   ├── _ViewImports.cshtml
        │   │   └── _ViewStart.cshtml
        │   └── wwwroot        
        │       └── images
        │           ├── cars
        │           └── homePage
        ├── AutoOglasi.Web.Constants
        └── AutoOglasi.Web.ViewModels
```

## 🤝 Contributing

Contributions are welcome! Here are several ways you can contribute:

- **[Submit Pull Requests](https://github.com/RRaleBG/AutoOglasi/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.
- **[Join the Discussions](https://github.com/RRaleBG/AutoOglasi/discussions)**: Share your insights, provide feedback, or ask questions.
- **[Report Issues](https://github.com/RRaleBG/AutoOglasi/issues)**: Submit bugs found or log feature requests for Autooglasi.

<details closed>
    <summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your GitHub account.
2. **Clone Locally**: Clone the forked repository to your local machine using a Git client.
   ```sh
   git clone https://github.com/RRaleBG/AutoOglasi
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to GitHub**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.

Once your PR is reviewed and approved, it will be merged into the main branch.

</details>

---

## 📄 License

This project is protected under the [MIT](https://choosealicense.com/licenses/mit/) License. For more details, refer to the [LICENSE](https://choosealicense.com/licenses/) file.

---

## 👏 Acknowledgments

- List any resources, contributors, inspiration, etc. here.

[**Return**](#-quick-links)

---
