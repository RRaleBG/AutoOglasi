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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(defaultConnection) || defaultConnection.Contains("YOUR_SQL_SERVER", StringComparison.Ordinal))
{
    throw new InvalidOperationException(
        "ConnectionStrings:DefaultConnection is not configured. Set a real SQL Server connection string in user secrets, environment variables, or appsettings.Development.json before starting the application.");
}

builder.Services.AddDbContext<AutoOglasiDbContext>(options =>
    options.UseSqlServer(defaultConnection));

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
    options.MinimumSameSitePolicy = SameSiteMode.None; // TEMP: Allow cross-origin cookies for troubleshooting
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

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

// Mapiranje za Areas (MVC controllere)
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default controller route
app.MapDefaultControllerRoute();

// 🔑 ključno za Identity Razor Pages
app.MapRazorPages();

await app.RunAsync();

public partial class Program;