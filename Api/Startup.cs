using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarWarsAPI.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace StarWarsAPI
{
    /// <summary>
    /// StartUp Class
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddMvcCore()
                .AddApiExplorer();

            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(x=> x.JsonSerializerOptions.IgnoreNullValues = true );

            services
                .AddLogging()
                .AddCustomServices()
                .AddOpenApi()
                .AddHealthChecks();
        }

        /// <summary>
        /// Configure API
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app                
                .UseIf(env.IsDevelopment(), appBuilder => appBuilder.UseDeveloperExceptionPage())
                .UseHealthChecks("/health")
                .UseSwagger()
                .UseRouting()
                .UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "StarWars API");
                    setup.DocExpansion(DocExpansion.None);

                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}");
            });
        }
    }
}
