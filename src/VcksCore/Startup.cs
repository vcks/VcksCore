using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

using VcksCore.DAL.EF;
using VcksCore.DAL.Entities;
using VcksCore.DAL.Repositories;
using VcksCore.BLL.Services;

using VcksCore.Models;

namespace VcksCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("LocalMsSqlString");

            //string connectionString = Configuration.GetConnectionString("AzureString");

            services.AddDbContext<VcksDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<VcksUser, VcksRole>().AddEntityFrameworkStores<VcksDbContext, int>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Login";
                options.Cookies.ApplicationCookie.LogoutPath = "/Logout";
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/Login";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<AccountManager>();
            services.AddScoped<DialogManager>();
            services.AddScoped<FileManager>();
            services.AddScoped<ProfileManager>();
            services.AddScoped<RelationshipManager>();
            services.AddScoped<WallManager>();

            services.AddScoped<AccountService>();
            services.AddScoped<DialogService>();
            services.AddScoped<FileService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<RelationshipService>();
            services.AddScoped<WallService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSignalR();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseRewriter(new RewriteOptions().AddRedirect("^(.*)/$", "$1").AddRedirect(@"^(home|account)/(\w+)$", @"$2"));

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context => context.Context.Response.Headers.Add("Cache-Control", "public, max-age=604800")
            });

            app.UseIdentity();

            app.UseWebSockets();
            app.UseSignalR();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ApiRelationshipRoute",
                    template: "api/{controller:regex(^Relationship$)}/{action}/{id}"
                );

                routes.MapRoute(
                  name: "AccountRoute",
                    template: "{action:regex(^Login|Logout|Register$)}",
                    defaults: new { controller = "Account" }
                );

                routes.MapRoute(
                   name: "FullAccountRoute",
                   template: "Account/{action=login}",
                   defaults: new { controller = "Account"}
                );

                routes.MapRoute(
                   name: "FileRoute",
                   template: "File/{id:guid}",
                   defaults: new { controller = "File", action = "Get" }
               );

                routes.MapRoute(
                    name: "Default",
                    template: "{action=Index}/{id?}",
                    defaults: new { controller = "Home"}
                );
            });
        }
    }
}
