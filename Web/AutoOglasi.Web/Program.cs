using AutoOglasi.Data;
using AutoOglasi.Data.Models;
using AutoOglasi.MapperConfigurations.Profiles;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Images;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Statistics;
using AutoOglasi.Web.Constants;
using AutoOglasi.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure; // Dodato za IMigrationsAssembly
using Microsoft.EntityFrameworkCore.Migrations; // Dodato za IMigrationsAssembly
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var dbProvider = builder.Configuration["DbProvider"];
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(defaultConnection) || defaultConnection.Contains("YOUR_SQL_SERVER", StringComparison.Ordinal))
{
    throw new InvalidOperationException(
        "ConnectionStrings:DefaultConnection is not configured. Set a real SQL Server connection string in user secrets, environment variables, or appsettings.Development.json before starting the application.");
}

builder.Services.AddDbContext<AutoOglasiDbContext>(options =>
{
    var mainAssembly = typeof(AutoOglasiDbContext).Assembly.FullName;
       
    options.UseSqlServer(defaultConnection, b =>
            b.MigrationsAssembly(mainAssembly));
    
    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});

//builder.Services.AddDbContext<AutoOglasiDbContext>(options =>
//{
//    var mainAssembly = typeof(AutoOglasiDbContext).Assembly.FullName;

//    // 1. Ako je na Azure produkciji (PostgreSQL)
//    if (dbProvider?.Equals("AZURE_POSTGRESQL_CONNECTIONSTRING", StringComparison.OrdinalIgnoreCase) == true)
//    {
//        options.UseNpgsql(defaultConnection, b => b.MigrationsAssembly(mainAssembly));
//    }
//    // 2. Ako je na lokalu (Development okruženje - SqlServer)
//    else
//    {
//        options.UseSqlServer(defaultConnection, b => b.MigrationsAssembly(mainAssembly));
//    }

//    // Isključujemo striktnu proveru modela za različite provajdere u runtime-u
//    options.ConfigureWarnings(warnings =>
//    {
//        warnings.Ignore(RelationalEventId.PendingModelChangesWarning);
//    });
//});

builder.Services.AddAutoMapper(_ => { }, typeof(CarsProfile).Assembly);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<AutoOglasiDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
});

var mvcBuilder = builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services.AddRazorPages();
builder.Services.AddTransient<ICarsService, CarsService>();
builder.Services.AddTransient<IPostsService, PostsService>();
builder.Services.AddTransient<IImagesService, ImagesService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();

var authenticationBuilder = builder.Services.AddAuthentication();
var googleClientId = builder.Configuration["Google:ClientId"];
var googleClientSecret = builder.Configuration["Google:ClientSecret"];

if (!string.IsNullOrWhiteSpace(googleClientId) && !string.IsNullOrWhiteSpace(googleClientSecret))
{
    authenticationBuilder.AddGoogle(options =>
    {
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
    });
}

var githubClientId = builder.Configuration["GitHub:ClientId"];
var githubClientSecret = builder.Configuration["GitHub:ClientSecret"];

if (!string.IsNullOrWhiteSpace(githubClientId) && !string.IsNullOrWhiteSpace(githubClientSecret))
{
    authenticationBuilder.AddGitHub(options =>
    {
        options.ClientId = githubClientId;
        options.ClientSecret = githubClientSecret;
    });
}

builder.Services.AddHealthChecks();

var app = builder.Build();

// Pokretanje automatskih migracija i seedovanja podataka
await app.InitializeDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePagesWithRedirects(WebConstants.StatusCodePath);
    app.UseExceptionHandler(WebConstants.ExceptionHandlerPath);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.MapHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapRazorPages();

await app.RunAsync();

public partial class Program;
