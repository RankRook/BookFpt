using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFpt.Areas.Identity.Data;
using BookFpt.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookFpt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<SampleAppContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("SampleAppContextConnection")));
            services.AddIdentity<SampleAppUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = true;

            })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<SampleAppContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<SampleAppUser>,
                ApplicationUserClaimsPrincipalFactory
                >();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EmailID", policy =>
                policy.RequireClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "support@procodeguide.com"
                ));

                options.AddPolicy("roleAdmin", policy =>
                policy.RequireRole("Admin")
                );
                options.AddPolicy("roleUser", policy =>
                policy.RequireRole("User")
                );
                options.AddPolicy("roleOwner", policy =>
                policy.RequireRole("Owner")
                );

            });

            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            //app.UseMvcWithDefaultRoute();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
