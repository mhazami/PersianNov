using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersianNov.Services;

namespace WebApp
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
            var type = Configuration.GetSection("SystemType").Value;
            var loginUrl = string.Empty;
            switch (type)
            {
                case "Author":
                    loginUrl = "/Author/Login";
                    break;
                case "Publisher":
                    loginUrl = "/Publisher/Login";
                    break;
                default:
                    loginUrl = "/ورود-مخاطب";
                    break;
            }
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = loginUrl;
                option.Cookie.Name = "User";
            });
            string connectionString = Configuration.GetConnectionString("PersianVonConnectionString");

            PersianNovComponent.ConnectionString = connectionString;
            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            var type = Configuration.GetSection("SystemType").Value;
            var pattern = string.Empty;
            switch (type)
            {
                case "Author":
                    pattern = "{controller=Author}/{action=Cartable}/{id?}";
                    break;
                case "Publisher":
                    pattern = "{controller=Publisher}/{action=Cartable}/{id?}";
                    break;
                default:
                    pattern = "{controller=Home}/{action=Index}/{id?}";
                    break;
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: pattern);
            });
        }
    }
}
