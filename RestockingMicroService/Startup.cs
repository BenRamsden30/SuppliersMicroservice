using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", Options =>
             {
                 Options.Authority = "https://thamco-auth-staging.azurewebsites.net/";
                 Options.Audience = "api_suppliers";
             });

            services.AddControllers();

            services.AddDbContext<RestockingMicroServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RestockingMicroServiceContext"), x =>
                    {
                        x.MigrationsHistoryTable("__EFMigrationsHistory", "Suppliers");
                        x.MigrationsHistoryTable("__EFMigrationsHistory", "Restocks");
                        x.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                            );
                    }));

            if (Enviro.IsDevelopment())
            {
                services.AddSingleton<SupplierInterface, SuppliersFakeProxy>();
                services.AddSingleton<RestocksInterface, RestockFakeProxy>();
            }
            else
            {
                services.AddScoped<SupplierInterface, SupplierRealProxy>();
                services.AddScoped<RestocksInterface, RestocksRealProxy>();
            }

            services.AddHttpClient("Supplier")
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                // HttpRequestException, 5XX and 408  
                .HandleTransientHttpError()
                // 404  
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                // Retry two times after delay  
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                ;
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
