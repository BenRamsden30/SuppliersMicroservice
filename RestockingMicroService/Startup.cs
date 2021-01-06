using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System.IdentityModel.Tokens.Jwt;

namespace RestockingMicroService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Enviro = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Enviro { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", Options =>
             {
                 Options.Authority = "https://thamco-auth-staging.azurewebsites.net/";
                 Options.Audience = "api_suppliers";
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("StaffOAuthorised", builder =>
                {
                    builder.RequireClaim("role", "Staff");
                });
            });

            services.AddControllers();

            services.AddDbContext<RestockingMicroServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RestockingMicroServiceContext"), x =>
                    {
                        x.MigrationsHistoryTable("__EFMigrationsHistory", "Suppliers");
                        x.MigrationsHistoryTable("__EFMigrationsHistory", "Restocks");
                    }));

            if (Enviro.IsDevelopment())
            {
                services.AddSingleton<SupplierInterface, SuppliersFakeProxy>();
                //Add fake for restocks
            }
            else
            {
                services.AddScoped<SupplierInterface, SupplierRealProxy>();
                //Add real for restocks
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
