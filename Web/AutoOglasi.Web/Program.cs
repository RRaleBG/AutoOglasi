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
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.IO;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var defaultConnection = ResolvePostgreSqlConnectionString(builder.Configuration);

if (string.IsNullOrWhiteSpace(defaultConnection) || defaultConnection.Contains("YOUR_PASSWORD", StringComparison.Ordinal))
{
    throw new InvalidOperationException(
        "A PostgreSQL connection string is not configured. Set either DefaultConnection or ConnectionStrings:DefaultConnection in user secrets, environment variables, or appsettings.Development.json before starting the application.");
}

ValidatePostgreSqlConnectionString(defaultConnection);

var allowedCorsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
var permitLimit = builder.Configuration.GetValue<int?>("RateLimiting:PermitLimit") ?? 120;
var windowSeconds = builder.Configuration.GetValue<int?>("RateLimiting:WindowSeconds") ?? 60;
var queueLimit = builder.Configuration.GetValue<int?>("RateLimiting:QueueLimit") ?? 0;

builder.Services.AddDbContext<AutoOglasiDbContext>(options =>
{
    var mainAssembly = typeof(AutoOglasiDbContext).Assembly.FullName;

    options.UseNpgsql(defaultConnection, b =>
            b.MigrationsAssembly(mainAssembly));

    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
});


builder.Services.AddAutoMapper(_ => { }, typeof(CarsProfile).Assembly);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownProxies.Clear();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        if (allowedCorsOrigins.Length > 0)
        {
            policy.WithOrigins(allowedCorsOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var requestPath = context.Request.Path;
        if (requestPath.StartsWithSegments("/health", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/css", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/js", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/lib", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/images", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/favicon.ico", StringComparison.OrdinalIgnoreCase)
            || requestPath.StartsWithSegments("/bootstrap-icons", StringComparison.OrdinalIgnoreCase)
            || Path.HasExtension(requestPath.Value))
        {
            return RateLimitPartition.GetNoLimiter("excluded-path");
        }

        var clientAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: clientAddress,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = permitLimit,
                Window = TimeSpan.FromSeconds(windowSeconds),
                QueueLimit = queueLimit,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                AutoReplenishment = true
            });
    });
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
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
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

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

var microsoftClientId = builder.Configuration["Microsoft:ClientId"];
var microsoftClientSecret = builder.Configuration["Microsoft:ClientSecret"];
if (!string.IsNullOrWhiteSpace(microsoftClientId) && !string.IsNullOrWhiteSpace(microsoftClientSecret))
{
    authenticationBuilder.AddMicrosoftAccount(options =>
    {
        options.ClientId = microsoftClientId;
        options.ClientSecret = microsoftClientSecret;
    });
}

builder.Services.AddHealthChecks();

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

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseCors("DefaultCorsPolicy");
app.UseRateLimiter();

app.MapHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapRazorPages();

await app.RunAsync();

static void ValidatePostgreSqlConnectionString(string connectionString)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

    try
    {
        _ = new NpgsqlConnectionStringBuilder(connectionString);
    }
    catch (ArgumentException ex)
    {
        throw new InvalidOperationException(
            "ConnectionStrings:DefaultConnection is not a valid PostgreSQL connection string. Check appsettings, environment variables, and user secrets for invalid or overridden values.",
            ex);
    }
}

static string? ResolvePostgreSqlConnectionString(IConfiguration configuration)
{
    var directConnectionString = configuration["DefaultConnection"];
    if (!string.IsNullOrWhiteSpace(directConnectionString))
    {
        return directConnectionString;
    }

    return configuration.GetConnectionString("DefaultConnection");
}

public partial class Program;
