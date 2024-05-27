
<p align="center">
  <img src="https://cdn-icons-png.flaticon.com/512/6295/6295417.png" width="100" />
</p>
<p align="center">
    <h1 align="center">AUTOOGLASI</h1>
</p>
<p align="center">
    <em><code>► INSERT-TEXT-HERE</code></em>
</p>
<p align="center">
	<img src="https://img.shields.io/github/license/RRaleBG/AutoOglasi?style=flat&color=0080ff" alt="license">
	<img src="https://img.shields.io/github/last-commit/RRaleBG/AutoOglasi?style=flat&logo=git&logoColor=white&color=0080ff" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/RRaleBG/AutoOglasi?style=flat&color=0080ff" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/RRaleBG/AutoOglasi?style=flat&color=0080ff" alt="repo-language-count">
<p>
<p align="center">
		<em>Developed with the software and tools below.</em>
</p>
<p align="center">
	<img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=flat&logo=JavaScript&logoColor=black" alt="JavaScript">
	<img src="https://img.shields.io/badge/YAML-CB171E.svg?style=flat&logo=YAML&logoColor=white" alt="YAML">
	<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat&logo=JSON&logoColor=white" alt="JSON">
</p>
<hr>

## 🔗 Quick Links

> - [📍 Overview](#-overview)
> - [📦 Features](#-features)
> - [📂 Repository Structure](#-repository-structure)
> - [🧩 Modules](#-modules)
> - [🚀 Getting Started](#-getting-started)
>   - [⚙️ Installation](#️-installation)
>   - [🤖 Running AutoOglasi](#-running-AutoOglasi)
>   - [🧪 Tests](#-tests)
> - [🛠 Project Roadmap](#-project-roadmap)
> - [🤝 Contributing](#-contributing)
> - [📄 License](#-license)
> - [👏 Acknowledgments](#-acknowledgments)

---

## 📍 Overview

<code>► INSERT-TEXT-HERE</code>

---

## 📦 Features

<code>► INSERT-TEXT-HERE</code>

---

## 📂 Repository Structure

```sh
└── AutoOglasi/    
    ├── Data
    │   ├── AutoOglasi.Data
    │   │   ├── AutoOglasi.Data.csproj
    │   │   ├── AutoOglasiDbContext.cs
    │   │   ├── Migrations
    │   │   │   ├── 20211204115605_FinalMigrationsCombined.Designer.cs
    │   │   │   ├── 20211204115605_FinalMigrationsCombined.cs
    │   │   │   ├── 20211207144643_RemoveUnusedImageRemoteUrlProperty.Designer.cs
    │   │   │   ├── 20211207144643_RemoveUnusedImageRemoteUrlProperty.cs
    │   │   │   ├── 20211209182657_PostEntityExtension.Designer.cs
    │   │   │   ├── 20211209182657_PostEntityExtension.cs
    │   │   │   ├── 20211209194318_MoveCarLocationPropertiesFromPostToCar.Designer.cs
    │   │   │   ├── 20211209194318_MoveCarLocationPropertiesFromPostToCar.cs
    │   │   │   ├── 20220128110234_AddingModifiedOnPropertyToPostEntity.Designer.cs
    │   │   │   ├── 20220128110234_AddingModifiedOnPropertyToPostEntity.cs
    │   │   │   ├── 20220207130708_AddingIsCoverImagePropertyToImageEntity.Designer.cs
    │   │   │   ├── 20220207130708_AddingIsCoverImagePropertyToImageEntity.cs
    │   │   │   ├── 20220208151849_AddingIsDeletedAndDeletedOnProperties.Designer.cs
    │   │   │   ├── 20220208151849_AddingIsDeletedAndDeletedOnProperties.cs
    │   │   │   ├── 20220411184416_addingAdministratorRole.Designer.cs
    │   │   │   ├── 20220411184416_addingAdministratorRole.cs
    │   │   │   ├── 20220413092939_AddingIsPublicPropertyToPostEntity.Designer.cs
    │   │   │   ├── 20220413092939_AddingIsPublicPropertyToPostEntity.cs
    │   │   │   └── AutoOglasiDbContextModelSnapshot.cs
    │   │   └── Seeding
    │   │       ├── AdministratorSeeder.cs
    │   │       ├── AutoOglasiDbContextSeeder.cs
    │   │       ├── CategoriesSeeder.cs
    │   │       ├── ExtraTypesSeeder.cs
    │   │       ├── FuelTypesSeeder.cs
    │   │       ├── ISeeder.cs
    │   │       └── TransmissionTypesSeeder.cs
    │   ├── AutoOglasi.Data.Common
    │   │   ├── AutoOglasi.Data.Common.csproj
    │   │   └── DataConstants.cs
    │   └── AutoOglasi.Data.Models
    │       ├── ApplicationUser.cs
    │       ├── AutoOglasi.Data.Models.csproj
    │       ├── Car.cs
    │       ├── CarExtra.cs
    │       ├── Category.cs
    │       ├── Extra.cs
    │       ├── ExtraType.cs
    │       ├── FuelType.cs
    │       ├── Image.cs
    │       ├── Post.cs
    │       ├── RoleEdit.cs
    │       ├── RoleModification.cs
    │       ├── RoleUserHelper.cs
    │       └── TransmissionType.cs
    ├── Infrastructure
    │   ├── AutoOglasi.CustomAttributes
    │   │   ├── AutoOglasi.CustomAttributes.csproj
    │   │   └── ValidationAttributes
    │   │       ├── RangeUntilCurrentYearAttribute.cs
    │   │       └── RangeWithCustomFormatAttribute.cs
    │   ├── AutoOglasi.GlobalConstants
    │   │   ├── AutoOglasi.GlobalConstants.csproj
    │   │   └── GlobalConstants.cs
    │   └── AutoOglasi.MapperConfigurations
    │       ├── AutoOglasi.MapperConfigurations.csproj
    │       └── Profiles
    │           ├── CarsProfile.cs
    │           ├── ImagesProfile.cs
    │           └── PostsProfile.cs
    ├── LICENSE.txt
    ├── README.md
    ├── Services
    │   └── AutoOglasi.Services
    │       ├── AutoOglasi.Services.csproj
    │       ├── Cars
    │       │   ├── CarsService.cs
    │       │   ├── ICarsService.cs
    │       │   └── Models
    │       │       ├── BaseCarDTO.cs
    │       │       ├── BaseCarInputModelDTO.cs
    │       │       ├── BaseCarSpecificationServiceModel.cs
    │       │       ├── CarByUserDTO.cs
    │       │       ├── CarExtrasServiceModel.cs
    │       │       ├── CarFormInputModelDTO.cs
    │       │       ├── CarInListDTO.cs
    │       │       ├── LatestPostsCarDTO.cs
    │       │       ├── SearchCarInputModelDTO.cs
    │       │       └── SingleCarDTO.cs
    │       ├── Images
    │       │   ├── IImagesService.cs
    │       │   ├── ImagesService.cs
    │       │   └── Models
    │       │       └── ImageInfoDTO.cs
    │       ├── Posts
    │       │   ├── IPostsService.cs
    │       │   ├── Models
    │       │   │   ├── BasePostInListDTO.cs
    │       │   │   ├── BasePostsListDTO.cs
    │       │   │   ├── EditPostDTO.cs
    │       │   │   ├── PostByUserDTO.cs
    │       │   │   ├── PostFormInputModelDTO.cs
    │       │   │   ├── PostInLatestListDTO.cs
    │       │   │   ├── PostInListDTO.cs
    │       │   │   ├── PostsByUserDTO.cs
    │       │   │   ├── PostsListDTO.cs
    │       │   │   ├── SearchPostDTO.cs
    │       │   │   └── SinglePostDTO.cs
    │       │   └── PostsService.cs
    │       └── Statistics
    │           ├── IStatisticsService.cs
    │           ├── Models
    │           │   └── StatisticsServiceModel.cs
    │           └── StatisticsService.cs
    ├── Users.txt
    └── Web
        ├── AutoOglasi.Web
        │   ├── Areas
        │   │   ├── Admin
        │   │   │   ├── AdminConstants.cs
        │   │   │   ├── Controllers
        │   │   │   └── Views
        │   │   └── Identity
        │   │       └── Pages
        │   ├── AutoOglasi.Web.csproj
        │   ├── Controllers
        │   │   ├── Api
        │   │   │   └── StatisticsApiController.cs
        │   │   ├── HomeController.cs
        │   │   └── PostsController.cs
        │   ├── Program.cs
        │   ├── Properties
        │   │   ├── launchSettings.json
        │   │   ├── serviceDependencies.json
        │   │   └── serviceDependencies.local.json
        │   ├── Startup.cs
        │   ├── Users.txt
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
        │   ├── appsettings.Development.json
        │   ├── appsettings.json
        │   ├── libman.json
        │   └── wwwroot        
        │       ├── css
        │       │   ├── myCss.css
        │       │   ├── owl.carousel.min.css
        │       │   └── owl.theme.default.min.css
        │       ├── favicon-16x16.png
        │       ├── favicon-32x32.png
        │       ├── favicon.ico
        │       ├── images
        │       │   ├── cars
        │           └── homePage
        ├── AutoOglasi.Web.Constants
        │   ├── AutoOglasi.Web.Constants.csproj
        │   └── WebConstants.cs
        └── AutoOglasi.Web.ViewModels
            ├── AutoOglasi.Web.ViewModels.csproj
            ├── Cars
            │   ├── BaseCarInputModel.cs
            │   ├── BaseCarSpecificationViewModel.cs
            │   ├── BaseCarViewModel.cs
            │   ├── CarByUserViewModel.cs
            │   ├── CarExtrasViewModel.cs
            │   ├── CarFormInputModel.cs
            │   ├── CarInListViewModel.cs
            │   ├── LatestPostsCarViewModel.cs
            │   ├── SearchCarInputModel.cs
            │   └── SingleCarViewModel.cs
            ├── ErrorViewModel.cs
            ├── Images
            │   └── ImageInfoViewModel.cs
            ├── PagingViewModel.cs
            ├── Posts
            │   ├── Contracts
            │   │   └── ISortableModel.cs
            │   ├── EditPostViewModel.cs
            │   ├── LatestPostsViewModel.cs
            │   ├── PostByUserViewModel.cs
            │   ├── PostFormInputModel.cs
            │   ├── PostInAdminAreaViewModel.cs
            │   ├── PostInLatestListViewModel.cs
            │   ├── PostInListViewModel.cs
            │   ├── PostsByUserViewModel.cs
            │   ├── PostsListAdminAreaViewModel.cs
            │   ├── PostsListViewModel.cs
            │   ├── SearchPostInputModel.cs
            │   └── SinglePostViewModel.cs
            └── PostsSorting.cs
```

---

## 🧩 Modules

<details closed><summary>.</summary>

| File                                                                               | Summary                         |
| ---                                                                                | ---                             |
| [LICENSE.txt](https://github.com/RRaleBG/AutoOglasi/blob/master/LICENSE.txt)       | <code>► INSERT-TEXT-HERE</code> |
| [Users.txt](https://github.com/RRaleBG/AutoOglasi/blob/master/Users.txt)           | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.sln](https://github.com/RRaleBG/AutoOglasi/blob/master/AutoOglasi.sln) | <code>► INSERT-TEXT-HERE</code> |
| [CodeMap1.dgml](https://github.com/RRaleBG/AutoOglasi/blob/master/CodeMap1.dgml)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Infrastructure.AutoOglasi.GlobalConstants</summary>

| File                                                                                                                                                               | Summary                         |
| ---                                                                                                                                                                | ---                             |
| [GlobalConstants.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.GlobalConstants/GlobalConstants.cs)                               | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.GlobalConstants.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.GlobalConstants/AutoOglasi.GlobalConstants.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Infrastructure.AutoOglasi.MapperConfigurations</summary>

| File                                                                                                                                                                              | Summary                         |
| ---                                                                                                                                                                               | ---                             |
| [AutoOglasi.MapperConfigurations.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.MapperConfigurations/AutoOglasi.MapperConfigurations.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Infrastructure.AutoOglasi.MapperConfigurations.Profiles</summary>

| File                                                                                                                                           | Summary                         |
| ---                                                                                                                                            | ---                             |
| [CarsProfile.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.MapperConfigurations/Profiles/CarsProfile.cs)     | <code>► INSERT-TEXT-HERE</code> |
| [PostsProfile.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.MapperConfigurations/Profiles/PostsProfile.cs)   | <code>► INSERT-TEXT-HERE</code> |
| [ImagesProfile.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.MapperConfigurations/Profiles/ImagesProfile.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Infrastructure.AutoOglasi.CustomAttributes</summary>

| File                                                                                                                                                                  | Summary                         |
| ---                                                                                                                                                                   | ---                             |
| [AutoOglasi.CustomAttributes.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.CustomAttributes/AutoOglasi.CustomAttributes.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Infrastructure.AutoOglasi.CustomAttributes.ValidationAttributes</summary>

| File                                                                                                                                                                                     | Summary                         |
| ---                                                                                                                                                                                      | ---                             |
| [RangeUntilCurrentYearAttribute.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.CustomAttributes/ValidationAttributes/RangeUntilCurrentYearAttribute.cs) | <code>► INSERT-TEXT-HERE</code> |
| [RangeWithCustomFormatAttribute.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Infrastructure/AutoOglasi.CustomAttributes/ValidationAttributes/RangeWithCustomFormatAttribute.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services</summary>

| File                                                                                                                                    | Summary                         |
| ---                                                                                                                                     | ---                             |
| [AutoOglasi.Services.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/AutoOglasi.Services.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Cars</summary>

| File                                                                                                                   | Summary                         |
| ---                                                                                                                    | ---                             |
| [ICarsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/ICarsService.cs) | <code>► INSERT-TEXT-HERE</code> |
| [CarsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/CarsService.cs)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Cars.Models</summary>

| File                                                                                                                                                                  | Summary                         |
| ---                                                                                                                                                                   | ---                             |
| [LatestPostsCarDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/LatestPostsCarDTO.cs)                               | <code>► INSERT-TEXT-HERE</code> |
| [CarFormInputModelDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/CarFormInputModelDTO.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [BaseCarDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/BaseCarDTO.cs)                                             | <code>► INSERT-TEXT-HERE</code> |
| [BaseCarSpecificationServiceModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/BaseCarSpecificationServiceModel.cs) | <code>► INSERT-TEXT-HERE</code> |
| [CarInListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/CarInListDTO.cs)                                         | <code>► INSERT-TEXT-HERE</code> |
| [CarExtrasServiceModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/CarExtrasServiceModel.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [SearchCarInputModelDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/SearchCarInputModelDTO.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [SingleCarDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/SingleCarDTO.cs)                                         | <code>► INSERT-TEXT-HERE</code> |
| [CarByUserDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/CarByUserDTO.cs)                                         | <code>► INSERT-TEXT-HERE</code> |
| [BaseCarInputModelDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Cars/Models/BaseCarInputModelDTO.cs)                         | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Images</summary>

| File                                                                                                                         | Summary                         |
| ---                                                                                                                          | ---                             |
| [ImagesService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Images/ImagesService.cs)   | <code>► INSERT-TEXT-HERE</code> |
| [IImagesService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Images/IImagesService.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Images.Models</summary>

| File                                                                                                                            | Summary                         |
| ---                                                                                                                             | ---                             |
| [ImageInfoDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Images/Models/ImageInfoDTO.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Statistics</summary>

| File                                                                                                                                     | Summary                         |
| ---                                                                                                                                      | ---                             |
| [IStatisticsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Statistics/IStatisticsService.cs) | <code>► INSERT-TEXT-HERE</code> |
| [StatisticsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Statistics/StatisticsService.cs)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Statistics.Models</summary>

| File                                                                                                                                                    | Summary                         |
| ---                                                                                                                                                     | ---                             |
| [StatisticsServiceModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Statistics/Models/StatisticsServiceModel.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Posts</summary>

| File                                                                                                                      | Summary                         |
| ---                                                                                                                       | ---                             |
| [IPostsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/IPostsService.cs) | <code>► INSERT-TEXT-HERE</code> |
| [PostsService.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/PostsService.cs)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Services.AutoOglasi.Services.Posts.Models</summary>

| File                                                                                                                                             | Summary                         |
| ---                                                                                                                                              | ---                             |
| [BasePostInListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/BasePostInListDTO.cs)         | <code>► INSERT-TEXT-HERE</code> |
| [EditPostDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/EditPostDTO.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [PostInLatestListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostInLatestListDTO.cs)     | <code>► INSERT-TEXT-HERE</code> |
| [PostsListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostsListDTO.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [PostInListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostInListDTO.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [SearchPostDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/SearchPostDTO.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [PostsByUserDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostsByUserDTO.cs)               | <code>► INSERT-TEXT-HERE</code> |
| [PostFormInputModelDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostFormInputModelDTO.cs) | <code>► INSERT-TEXT-HERE</code> |
| [BasePostsListDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/BasePostsListDTO.cs)           | <code>► INSERT-TEXT-HERE</code> |
| [SinglePostDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/SinglePostDTO.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [PostByUserDTO.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Services/AutoOglasi.Services/Posts/Models/PostByUserDTO.cs)                 | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web</summary>

| File                                                                                                                              | Summary                         |
| ---                                                                                                                               | ---                             |
| [appsettings.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/appsettings.json)                         | <code>► INSERT-TEXT-HERE</code> |
| [Startup.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Startup.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [appsettings.Development.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/appsettings.Development.json) | <code>► INSERT-TEXT-HERE</code> |
| [Users.txt](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Users.txt)                                       | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.Web.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/AutoOglasi.Web.csproj)               | <code>► INSERT-TEXT-HERE</code> |
| [Program.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Program.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [libman.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/libman.json)                                   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Admin</summary>

| File                                                                                                                    | Summary                         |
| ---                                                                                                                     | ---                             |
| [AdminConstants.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/AdminConstants.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Admin.Views</summary>

| File                                                                                                                              | Summary                         |
| ---                                                                                                                               | ---                             |
| [_ViewImports.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/_ViewImports.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [_ViewStart.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/_ViewStart.cshtml)     | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Admin.Views.Role</summary>

| File                                                                                                                       | Summary                         |
| ---                                                                                                                        | ---                             |
| [Update.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/Role/Update.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [Create.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/Role/Create.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [Index.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/Role/Index.cshtml)   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Admin.Views.Posts</summary>

| File                                                                                                                  | Summary                         |
| ---                                                                                                                   | ---                             |
| [All.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Views/Posts/All.cshtml) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Admin.Controllers</summary>

| File                                                                                                                                  | Summary                         |
| ---                                                                                                                                   | ---                             |
| [RoleController.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Controllers/RoleController.cs)   | <code>► INSERT-TEXT-HERE</code> |
| [PostsController.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Admin/Controllers/PostsController.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Areas.Identity.Pages</summary>

| File                                                                                                                             | Summary                         |
| ---                                                                                                                              | ---                             |
| [_ViewStart.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Areas/Identity/Pages/_ViewStart.cshtml) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Properties</summary>

| File                                                                                                                                             | Summary                         |
| ---                                                                                                                                              | ---                             |
| [launchSettings.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Properties/launchSettings.json)                       | <code>► INSERT-TEXT-HERE</code> |
| [serviceDependencies.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Properties/serviceDependencies.json)             | <code>► INSERT-TEXT-HERE</code> |
| [serviceDependencies.local.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Properties/serviceDependencies.local.json) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Views</summary>

| File                                                                                                                  | Summary                         |
| ---                                                                                                                   | ---                             |
| [_ViewImports.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/_ViewImports.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [_ViewStart.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/_ViewStart.cshtml)     | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Views.Home</summary>

| File                                                                                                         | Summary                         |
| ---                                                                                                          | ---                             |
| [About.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Home/About.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [Index.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Home/Index.cshtml) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Views.Posts</summary>

| File                                                                                                            | Summary                         |
| ---                                                                                                             | ---                             |
| [All.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/All.cshtml)       | <code>► INSERT-TEXT-HERE</code> |
| [Offer.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Offer.cshtml)   | <code>► INSERT-TEXT-HERE</code> |
| [Delete.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Delete.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [Mine.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Mine.cshtml)     | <code>► INSERT-TEXT-HERE</code> |
| [Edit.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Edit.cshtml)     | <code>► INSERT-TEXT-HERE</code> |
| [Create.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Create.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [Search.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Posts/Search.cshtml) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Views.Shared</summary>

| File                                                                                                                                                   | Summary                         |
| ---                                                                                                                                                    | ---                             |
| [Error.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/Error.cshtml)                                         | <code>► INSERT-TEXT-HERE</code> |
| [_Favicons.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_Favicons.cshtml)                                 | <code>► INSERT-TEXT-HERE</code> |
| [_ValidationScriptsPartial.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_ValidationScriptsPartial.cshtml) | <code>► INSERT-TEXT-HERE</code> |
| [_Layout.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_Layout.cshtml)                                     | <code>► INSERT-TEXT-HERE</code> |
| [_SortingPartial.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_SortingPartial.cshtml)                     | <code>► INSERT-TEXT-HERE</code> |
| [_PagingPartial.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_PagingPartial.cshtml)                       | <code>► INSERT-TEXT-HERE</code> |
| [_LoginPartial.cshtml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Views/Shared/_LoginPartial.cshtml)                         | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot</summary>

| File                                                                                                              | Summary                         |
| ---                                                                                                               | ---                             |
| [site.webmanifest](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/site.webmanifest) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.Roboto</summary>

| File                                                                                                           | Summary                         |
| ---                                                                                                            | ---                             |
| [LICENSE.txt](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/Roboto/LICENSE.txt) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.css</summary>

| File                                                                                                                                    | Summary                         |
| ---                                                                                                                                     | ---                             |
| [owl.carousel.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/css/owl.carousel.min.css)           | <code>► INSERT-TEXT-HERE</code> |
| [owl.theme.default.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/css/owl.theme.default.min.css) | <code>► INSERT-TEXT-HERE</code> |
| [myCss.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/css/myCss.css)                                 | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.lib.fontawesome.css</summary>

| File                                                                                                                        | Summary                         |
| ---                                                                                                                         | ---                             |
| [all.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/fontawesome/css/all.css)         | <code>► INSERT-TEXT-HERE</code> |
| [all.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/fontawesome/css/all.min.css) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.lib.bootstrap.css</summary>

| File                                                                                                                                                                      | Summary                         |
| ---                                                                                                                                                                       | ---                             |
| [bootstrap-grid.rtl.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.rtl.css.map)                   | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.rtl.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.rtl.css.map)                             | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.css.map)                           | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.min.css)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.rtl.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.rtl.min.css)         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.css.map)                 | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.min.css.map)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.min.css.map)                             | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.css)                                   | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.css)                                             | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.rtl.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.rtl.css)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.rtl.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.rtl.min.css.map)           | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.rtl.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.rtl.css)                           | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.rtl.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.rtl.min.css.map)                     | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.min.css)                           | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.rtl.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.rtl.css)                 | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.css)                               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.css)                         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.min.css.map)                   | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.rtl.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.rtl.min.css)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.css.map)                                     | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.rtl.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.rtl.min.css.map) | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.rtl.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.rtl.min.css.map)       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.rtl.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.rtl.css)                                     | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.css.map)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.rtl.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.rtl.min.css)                             | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-reboot.rtl.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-reboot.rtl.css.map)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.min.css)                 | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-grid.rtl.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-grid.rtl.min.css)                   | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.rtl.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.rtl.css.map)         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap-utilities.min.css.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap-utilities.min.css.map)         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.min.css](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/css/bootstrap.min.css)                                     | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.lib.bootstrap.js</summary>

| File                                                                                                                                                     | Summary                         |
| ---                                                                                                                                                      | ---                             |
| [bootstrap.esm.min.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.esm.min.js)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.js)                               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.esm.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.esm.js.map)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.bundle.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.bundle.js.map)         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.bundle.min.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.bundle.min.js.map) | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.min.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.min.js)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.esm.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.esm.js)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.min.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.min.js.map)               | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.esm.min.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.esm.min.js.map)       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.bundle.min.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.bundle.min.js)         | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.js.map](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.js.map)                       | <code>► INSERT-TEXT-HERE</code> |
| [bootstrap.bundle.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/lib/bootstrap/js/bootstrap.bundle.js)                 | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.js.owlcarousel</summary>

| File                                                                                                                                   | Summary                         |
| ---                                                                                                                                    | ---                             |
| [owl.carousel.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/owl.carousel.js)         | <code>► INSERT-TEXT-HERE</code> |
| [.travis.yml](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/.travis.yml)                 | <code>► INSERT-TEXT-HERE</code> |
| [owl.carousel.min.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/owl.carousel.min.js) | <code>► INSERT-TEXT-HERE</code> |
| [Gruntfile.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/Gruntfile.js)               | <code>► INSERT-TEXT-HERE</code> |
| [bower.json](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/bower.json)                   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.js.owlcarousel.src.scss</summary>

| File                                                                                                                                                  | Summary                         |
| ---                                                                                                                                                   | ---                             |
| [_theme.default.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_theme.default.scss)       | <code>► INSERT-TEXT-HERE</code> |
| [_theme.green.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_theme.green.scss)           | <code>► INSERT-TEXT-HERE</code> |
| [owl.carousel.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/owl.carousel.scss)           | <code>► INSERT-TEXT-HERE</code> |
| [_lazyload.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_lazyload.scss)                 | <code>► INSERT-TEXT-HERE</code> |
| [_theme.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_theme.scss)                       | <code>► INSERT-TEXT-HERE</code> |
| [owl.theme.default.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/owl.theme.default.scss) | <code>► INSERT-TEXT-HERE</code> |
| [_video.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_video.scss)                       | <code>► INSERT-TEXT-HERE</code> |
| [_autoheight.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_autoheight.scss)             | <code>► INSERT-TEXT-HERE</code> |
| [_animate.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_animate.scss)                   | <code>► INSERT-TEXT-HERE</code> |
| [owl.theme.green.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/owl.theme.green.scss)     | <code>► INSERT-TEXT-HERE</code> |
| [_core.scss](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/scss/_core.scss)                         | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.wwwroot.js.owlcarousel.src.js</summary>

| File                                                                                                                                                    | Summary                         |
| ---                                                                                                                                                     | ---                             |
| [owl.support.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.support.js)                     | <code>► INSERT-TEXT-HERE</code> |
| [owl.animate.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.animate.js)                     | <code>► INSERT-TEXT-HERE</code> |
| [owl.carousel.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.carousel.js)                   | <code>► INSERT-TEXT-HERE</code> |
| [owl.lazyload.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.lazyload.js)                   | <code>► INSERT-TEXT-HERE</code> |
| [owl.support.modernizr.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.support.modernizr.js) | <code>► INSERT-TEXT-HERE</code> |
| [owl.autorefresh.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.autorefresh.js)             | <code>► INSERT-TEXT-HERE</code> |
| [owl.hash.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.hash.js)                           | <code>► INSERT-TEXT-HERE</code> |
| [owl.video.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.video.js)                         | <code>► INSERT-TEXT-HERE</code> |
| [owl.autoheight.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.autoheight.js)               | <code>► INSERT-TEXT-HERE</code> |
| [owl.autoplay.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.autoplay.js)                   | <code>► INSERT-TEXT-HERE</code> |
| [.jscsrc](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/.jscsrc)                                   | <code>► INSERT-TEXT-HERE</code> |
| [.jshintrc](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/.jshintrc)                               | <code>► INSERT-TEXT-HERE</code> |
| [owl.navigation.js](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/wwwroot/js/owlcarousel/src/js/owl.navigation.js)               | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Controllers</summary>

| File                                                                                                                      | Summary                         |
| ---                                                                                                                       | ---                             |
| [HomeController.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Controllers/HomeController.cs)   | <code>► INSERT-TEXT-HERE</code> |
| [PostsController.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Controllers/PostsController.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Controllers.Api</summary>

| File                                                                                                                                          | Summary                         |
| ---                                                                                                                                           | ---                             |
| [StatisticsApiController.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web/Controllers/Api/StatisticsApiController.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.ViewModels</summary>

| File                                                                                                                                                 | Summary                         |
| ---                                                                                                                                                  | ---                             |
| [ErrorViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/ErrorViewModel.cs)                               | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.Web.ViewModels.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/AutoOglasi.Web.ViewModels.csproj) | <code>► INSERT-TEXT-HERE</code> |
| [PagingViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/PagingViewModel.cs)                             | <code>► INSERT-TEXT-HERE</code> |
| [PostsSorting.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/PostsSorting.cs)                                   | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.ViewModels.Cars</summary>

| File                                                                                                                                                      | Summary                         |
| ---                                                                                                                                                       | ---                             |
| [BaseCarInputModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/BaseCarInputModel.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [CarFormInputModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/CarFormInputModel.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [BaseCarSpecificationViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/BaseCarSpecificationViewModel.cs) | <code>► INSERT-TEXT-HERE</code> |
| [BaseCarViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/BaseCarViewModel.cs)                           | <code>► INSERT-TEXT-HERE</code> |
| [SearchCarInputModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/SearchCarInputModel.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [SingleCarViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/SingleCarViewModel.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [CarByUserViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/CarByUserViewModel.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [CarInListViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/CarInListViewModel.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [CarExtrasViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/CarExtrasViewModel.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [LatestPostsCarViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Cars/LatestPostsCarViewModel.cs)             | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.ViewModels.Images</summary>

| File                                                                                                                                  | Summary                         |
| ---                                                                                                                                   | ---                             |
| [ImageInfoViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Images/ImageInfoViewModel.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.ViewModels.Posts</summary>

| File                                                                                                                                                   | Summary                         |
| ---                                                                                                                                                    | ---                             |
| [PostsListViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostsListViewModel.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [LatestPostsViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/LatestPostsViewModel.cs)               | <code>► INSERT-TEXT-HERE</code> |
| [PostFormInputModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostFormInputModel.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [PostInListViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostInListViewModel.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [SinglePostViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/SinglePostViewModel.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [PostByUserViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostByUserViewModel.cs)                 | <code>► INSERT-TEXT-HERE</code> |
| [PostInLatestListViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostInLatestListViewModel.cs)     | <code>► INSERT-TEXT-HERE</code> |
| [EditPostViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/EditPostViewModel.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [PostsListAdminAreaViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostsListAdminAreaViewModel.cs) | <code>► INSERT-TEXT-HERE</code> |
| [PostInAdminAreaViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostInAdminAreaViewModel.cs)       | <code>► INSERT-TEXT-HERE</code> |
| [SearchPostInputModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/SearchPostInputModel.cs)               | <code>► INSERT-TEXT-HERE</code> |
| [PostsByUserViewModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/PostsByUserViewModel.cs)               | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.ViewModels.Posts.Contracts</summary>

| File                                                                                                                                   | Summary                         |
| ---                                                                                                                                    | ---                             |
| [ISortableModel.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.ViewModels/Posts/Contracts/ISortableModel.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Web.AutoOglasi.Web.Constants</summary>

| File                                                                                                                                              | Summary                         |
| ---                                                                                                                                               | ---                             |
| [WebConstants.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.Constants/WebConstants.cs)                                 | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.Web.Constants.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Web/AutoOglasi.Web.Constants/AutoOglasi.Web.Constants.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Data.AutoOglasi.Data.Common</summary>

| File                                                                                                                                         | Summary                         |
| ---                                                                                                                                          | ---                             |
| [DataConstants.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Common/DataConstants.cs)                           | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.Data.Common.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Common/AutoOglasi.Data.Common.csproj) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Data.AutoOglasi.Data.Models</summary>

| File                                                                                                                                         | Summary                         |
| ---                                                                                                                                          | ---                             |
| [Image.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/Image.cs)                                           | <code>► INSERT-TEXT-HERE</code> |
| [ExtraType.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/ExtraType.cs)                                   | <code>► INSERT-TEXT-HERE</code> |
| [Post.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/Post.cs)                                             | <code>► INSERT-TEXT-HERE</code> |
| [TransmissionType.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/TransmissionType.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [RoleUserHelper.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/RoleUserHelper.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [RoleModification.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/RoleModification.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [FuelType.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/FuelType.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [ApplicationUser.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/ApplicationUser.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [CarExtra.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/CarExtra.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [Car.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/Car.cs)                                               | <code>► INSERT-TEXT-HERE</code> |
| [Category.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/Category.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasi.Data.Models.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/AutoOglasi.Data.Models.csproj) | <code>► INSERT-TEXT-HERE</code> |
| [Extra.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/Extra.cs)                                           | <code>► INSERT-TEXT-HERE</code> |
| [RoleEdit.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data.Models/RoleEdit.cs)                                     | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Data.AutoOglasi.Data</summary>

| File                                                                                                                    | Summary                         |
| ---                                                                                                                     | ---                             |
| [AutoOglasi.Data.csproj](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/AutoOglasi.Data.csproj) | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasiDbContext.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/AutoOglasiDbContext.cs) | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Data.AutoOglasi.Data.Seeding</summary>

| File                                                                                                                                        | Summary                         |
| ---                                                                                                                                         | ---                             |
| [ISeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/ISeeder.cs)                                     | <code>► INSERT-TEXT-HERE</code> |
| [AdministratorSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/AdministratorSeeder.cs)             | <code>► INSERT-TEXT-HERE</code> |
| [ExtraTypesSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/ExtraTypesSeeder.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [CategoriesSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/CategoriesSeeder.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasiDbContextSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/AutoOglasiDbContextSeeder.cs) | <code>► INSERT-TEXT-HERE</code> |
| [FuelTypesSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/FuelTypesSeeder.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [TransmissionTypesSeeder.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Seeding/TransmissionTypesSeeder.cs)     | <code>► INSERT-TEXT-HERE</code> |

</details>

<details closed><summary>Data.AutoOglasi.Data.Migrations</summary>

| File                                                                                                                                                                                                                       | Summary                         |
| ---                                                                                                                                                                                                                        | ---                             |
| [20211209182657_PostEntityExtension.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211209182657_PostEntityExtension.Designer.cs)                                         | <code>► INSERT-TEXT-HERE</code> |
| [AutoOglasiDbContextModelSnapshot.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/AutoOglasiDbContextModelSnapshot.cs)                                                               | <code>► INSERT-TEXT-HERE</code> |
| [20211207144643_RemoveUnusedImageRemoteUrlProperty.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211207144643_RemoveUnusedImageRemoteUrlProperty.Designer.cs)           | <code>► INSERT-TEXT-HERE</code> |
| [20211204115605_FinalMigrationsCombined.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211204115605_FinalMigrationsCombined.Designer.cs)                                 | <code>► INSERT-TEXT-HERE</code> |
| [20220413092939_AddingIsPublicPropertyToPostEntity.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220413092939_AddingIsPublicPropertyToPostEntity.cs)                             | <code>► INSERT-TEXT-HERE</code> |
| [20211207144643_RemoveUnusedImageRemoteUrlProperty.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211207144643_RemoveUnusedImageRemoteUrlProperty.cs)                             | <code>► INSERT-TEXT-HERE</code> |
| [20211209194318_MoveCarLocationPropertiesFromPostToCar.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211209194318_MoveCarLocationPropertiesFromPostToCar.cs)                     | <code>► INSERT-TEXT-HERE</code> |
| [20220207130708_AddingIsCoverImagePropertyToImageEntity.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220207130708_AddingIsCoverImagePropertyToImageEntity.cs)                   | <code>► INSERT-TEXT-HERE</code> |
| [20220208151849_AddingIsDeletedAndDeletedOnProperties.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220208151849_AddingIsDeletedAndDeletedOnProperties.cs)                       | <code>► INSERT-TEXT-HERE</code> |
| [20220128110234_AddingModifiedOnPropertyToPostEntity.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220128110234_AddingModifiedOnPropertyToPostEntity.cs)                         | <code>► INSERT-TEXT-HERE</code> |
| [20211204115605_FinalMigrationsCombined.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211204115605_FinalMigrationsCombined.cs)                                                   | <code>► INSERT-TEXT-HERE</code> |
| [20211209182657_PostEntityExtension.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211209182657_PostEntityExtension.cs)                                                           | <code>► INSERT-TEXT-HERE</code> |
| [20220413092939_AddingIsPublicPropertyToPostEntity.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220413092939_AddingIsPublicPropertyToPostEntity.Designer.cs)           | <code>► INSERT-TEXT-HERE</code> |
| [20220207130708_AddingIsCoverImagePropertyToImageEntity.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220207130708_AddingIsCoverImagePropertyToImageEntity.Designer.cs) | <code>► INSERT-TEXT-HERE</code> |
| [20220128110234_AddingModifiedOnPropertyToPostEntity.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220128110234_AddingModifiedOnPropertyToPostEntity.Designer.cs)       | <code>► INSERT-TEXT-HERE</code> |
| [20211209194318_MoveCarLocationPropertiesFromPostToCar.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20211209194318_MoveCarLocationPropertiesFromPostToCar.Designer.cs)   | <code>► INSERT-TEXT-HERE</code> |
| [20220411184416_addingAdministratorRole.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220411184416_addingAdministratorRole.cs)                                                   | <code>► INSERT-TEXT-HERE</code> |
| [20220208151849_AddingIsDeletedAndDeletedOnProperties.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220208151849_AddingIsDeletedAndDeletedOnProperties.Designer.cs)     | <code>► INSERT-TEXT-HERE</code> |
| [20220411184416_addingAdministratorRole.Designer.cs](https://github.com/RRaleBG/AutoOglasi/blob/master/Data/AutoOglasi.Data/Migrations/20220411184416_addingAdministratorRole.Designer.cs)                                 | <code>► INSERT-TEXT-HERE</code> |

</details>

---

## 🚀 Getting Started

***Requirements***

Ensure you have the following dependencies installed on your system:

* **CSharp**: `version x.y.z`

### ⚙️ Installation

1. Clone the AutoOglasi repository:

```sh
git clone https://github.com/RRaleBG/AutoOglasi
```

2. Change to the project directory:

```sh
cd AutoOglasi
```

3. Install the dependencies:

```sh
dotnet build
```

### 🤖 Running AutoOglasi

Use the following command to run AutoOglasi:

```sh
dotnet run
```

### 🧪 Tests

To execute tests, run:

```sh
dotnet test
```

---

## 🛠 Project Roadmap

- [X] `► INSERT-TASK-1`
- [ ] `► INSERT-TASK-2`
- [ ] `► ...`

---

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

This project is protected under the [SELECT-A-LICENSE](https://choosealicense.com/licenses) License. For more details, refer to the [LICENSE](https://choosealicense.com/licenses/) file.

---

## 👏 Acknowledgments

- List any resources, contributors, inspiration, etc. here.

[**Return**](#-quick-links)

---
