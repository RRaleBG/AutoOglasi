    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AutoOglasi.Data;
    using AutoOglasi.Data.Models;
    using AutoOglasi.Data.Seeding;
    using AutoOglasi.Web.Constants;
    using AutoOglasi.Services.Cars;
    using AutoOglasi.Services.Images;
    using AutoOglasi.Services.Posts;
    using AutoOglasi.Services.Statistics;
    using AutoOglasi.MapperConfigurations.Profiles;
    using System;
namespace AutoOglasi.Web
{

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AutoOglasiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false; 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            })
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<AutoOglasiDbContext>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true; //without this configuration the TempData doesn't works
            });

            services.AddAutoMapper(typeof(CarsProfile).GetTypeInfo().Assembly);

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
                .AddRazorRuntimeCompilation();

            services.AddTransient<ICarsService, CarsService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AutoOglasiDbContext>();
                dbContext.Database.Migrate();   // equivalent Update-Database -TargetMigration  automatically update the database after a model changes

                new AutoOglasiDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
