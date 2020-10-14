using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportskeAktivnosti.Models;
using SportskeAktivnosti.Models.Database;
using SportskeAktivnosti.Models.Repository;
using SportskeAktivnosti.Models.Repository.Interfaces;

namespace SportskeAktivnosti {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // dodavanje konekcionog stringa za sql server
            services.AddDbContextPool<AppDbContext> (
                options => options.UseSqlServer (Configuration.GetConnectionString ("SportActivityDBConnection")));

            // dodavanje Identity Frameworka i konfiguracija passworda
            services.AddIdentity<IdentityUser, IdentityRole> (options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext> ();

            // dodavanje authorization
            services.AddControllersWithViews (options => {
                    var policy = new AuthorizationPolicyBuilder ()
                        .RequireAuthenticatedUser ()
                        .Build ();
                    options.Filters.Add (new AuthorizeFilter (policy));
                })
                .AddXmlSerializerFormatters ();

            // Menjanje default rute za AccessDenied
            services.ConfigureApplicationCookie (options => {
                options.AccessDeniedPath = new PathString ("/Administration/AccessDenied");
            });

            // 
            services.AddScoped<ISportObjectRepository, SQLSportObjectRepository> ();
            services.AddScoped<ITournamentRepository, SQLTournamentRepository> ();
            services.AddScoped<ISportSchoolRepository, SQLSportSchoolRepository> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // global exception handling za production
                app.UseExceptionHandler ("/Home/Error");

                // status code greske, custom - za production
                app.UseStatusCodePagesWithReExecute ("/Error/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseAuthentication ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}