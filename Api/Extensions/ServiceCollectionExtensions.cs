using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using StarWarsAPI.Application;
using StarWarsAPI.Domain.Models.CharactersAggregate;
using StarWarsAPI.Domain.Models.PlanetsAggregate;
using StarWarsAPI.Domain.Models.SpeciesAggregate;
using StarWarsAPI.Domain.Models.StarshipsAggregate;
using StarWarsAPI.Domain.Models.SyncAggregate;
using StarWarsAPI.Domain.Models.VehiclesAggregate;
using StarWarsAPI.Infrastructure.Repositories;
using StarWarsAPI.Infrastructure.Service;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace StarWarsAPI.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add same or different instances of the service to the requesting class.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {

            services
             .AddScoped<ICharactersRepository, CharactersRepository>()
             .AddScoped<IPlanetsRepository, PlanetsRepository>()
             .AddScoped<ISpeciesRepository, SpeciesRepository>()
             .AddScoped<IStarshipsRepository, StarshipsRepository>()
             .AddScoped<IVehiclesRepository, VehiclesRepository>()
             .AddHostedService<QueuedSyncService>()
             .AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            return services;
        }

        /// <summary>
        /// Add personalized http client to the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
        {
            services
                .AddHttpClient<ISyncRepository, SyncRepository>()
                .AddPolicyHandler(GetPolicyTimeOut())
                .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        /// <summary>
        ///Region of the Http polices retries   
        /// </summary>
        /// <returns></returns>
        #region HttpRetriesPolices
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetPolicyTimeOut()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(1);
        }
        #endregion

        /// <summary>
        /// Add tool to describe, produce, consume and visualize RESTFul APIs.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "StarWars API",
                        Version = "v1",
                        TermsOfService = new Uri("https://github.com/bluekiri/CharlaUIB2019"),
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Bluekiri",
                            Url = new Uri("https://bluekiri.com/")
                        }
                    });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));
                c.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }
    }
}
