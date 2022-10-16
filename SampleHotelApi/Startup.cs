using FluentValidation.AspNetCore;
using Kros.AspNetCore;
using Kros.AspNetCore.HealthChecks;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.BuilderMiddlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using SampleHotelApi.Application.Services;
using SampleHotelApi.Domain;
using SampleHotelApi.Infrastructure;
using SampleHotelApi.Infrastructure.DbMock;
using SampleHotelApi.Options;
using System.Reflection;

namespace SampleHotelApi
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup : BaseStartup
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="env">Environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
            : base(configuration, env)
        { }

        /// <summary>
        /// Configure IoC container.
        /// </summary>
        /// <param name="services">Service.</param>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidation();

            services.AddJwtAuthentication(Configuration.GetSection("ApiJwtAuthorization").Get<ApiJwtAuthorizationSettings>());

            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IActiveUserInfoService, ActiveUserInfoService>()
                .AddSingleton<IDatabase, Database>()
                .AddScoped<ICommentRepository, CommentRepository>()
                .AddScoped<IRoomRepository, RoomRepository>()
                .AddScoped<IReservationRepository, ReservationRepository>();

            services
                .AddSwaggerDocumentation(Configuration, options =>
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath, true);
                    options.CustomSchemaIds(s => s.FullName?.Replace("+", "."));

                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Na získanie autorizačného tokenu zadaj prihlasovacie meno a heslo.\nDostupní používatelia:\n" +
                            " - username: **alice**, password: *password*\n" +
                            " - username: **bob**, password: *password*",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Password = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri("http://localhost:5001/connect/token")
                            }
                        }
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                            },
                            Array.Empty<string>()
                        }
                    });
                })
                .AddFluentValidationRulesToSwagger();
            services.AddBasicHealthChecks();
        }

        /// <summary>
        /// Configure web api pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public override void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if (Environment.IsTestOrDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API v1");
                    c.OAuthClientId("swagger");
                    c.OAuthClientSecret("secret");
                    c.OAuthUsername("alice");
                    c.OAuthAppName("Sample hotel API - Swagger");
                });
            }

            app.UseRouting();
            app.UseErrorHandling();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponseAsync
                });

                endpoints
                    .MapControllers()
                    .RequireAuthorization("ApiScope");
            });
        }
    }
}
