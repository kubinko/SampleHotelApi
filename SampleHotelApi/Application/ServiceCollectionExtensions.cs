using FluentValidation.AspNetCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for registering services for this project to the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string SwaggerDocumentationSectionName = "SwaggerDocumentation";
        private const string ApiName = "Hotel API v1";

        /// <summary>
        /// Registers FluentValidation.
        /// </summary>
        /// <param name="builder">MVC builder.</param>
        /// <returns>MVC builder.</returns>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder builder)
            => builder.AddFluentValidation(o =>
            {
                o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                o.DisableDataAnnotationsValidation = true;
            });

        /// <summary>
        /// Adds services.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddServices(this IServiceCollection services)
            => services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        /// <summary>
        /// Adds basic health checks.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddBasicHealthChecks(this IServiceCollection services)
            => services.AddHealthChecks()
                .AddCheck(ApiName, _ => HealthCheckResult.Healthy(), tags: new[] { "api" });

        /// <summary>
        /// Registers Swagger documentation generator to IoC container.
        /// </summary>
        /// <param name="services">IoC container.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="setupAction">Action for configuring swagger generating options.</param>
        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<SwaggerGenOptions>? setupAction = null)
        {
            OpenApiInfo? swaggerDocumentationSettings = GetSwaggerDocumentationSettings(configuration);

            if (swaggerDocumentationSettings is null)
            {
                return services;
            }

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName); // https://wegotcode.com/microsoft/swagger-fix-for-dotnetcore/
            });

            string assemblyName = AppDomain.CurrentDomain.FriendlyName;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerDocumentationSettings.Version, swaggerDocumentationSettings);

                string documentationFilePath = GetXmlDocumentationFilePath(assemblyName);
                if (File.Exists(documentationFilePath))
                {
                    c.IncludeXmlComments(documentationFilePath);
                }

                AddSwaggerSecurity(c, swaggerDocumentationSettings);

                setupAction?.Invoke(c);
            });

            return services;
        }

        private static void AddSwaggerSecurity(SwaggerGenOptions swaggerOptions, OpenApiInfo swaggerDocumentationSettings)
        {
            if (swaggerDocumentationSettings.Extensions.TryGetValue("TokenUrl", out var t) && t is OpenApiString tokenUrl)
            {
                swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            TokenUrl = tokenUrl == null ? null : new Uri(tokenUrl.Value)
                        }
                    }
                });
                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            }
        }

        private static OpenApiInfo? GetSwaggerDocumentationSettings(IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection(SwaggerDocumentationSectionName);

            return configurationSection.Exists() ? configurationSection.Get<OpenApiInfo>() : null;
        }

        private static string GetXmlDocumentationFilePath(string assemblyName)
            => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".xml");
    }
}
