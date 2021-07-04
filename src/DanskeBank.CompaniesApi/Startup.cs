using Autofac;
using Autofac.Configuration;
using DanskeBank.Application.Api.Health;
using DanskeBank.CompaniesApi.AuthorizationPolicies;
using DanskeBank.CompaniesApi.Diagnostic.Health;
using DanskeBank.CompaniesApi.Filters;
using DanskeBank.Infrastructure.Data.Sql.Companies;
using DanskeBank.Infrastructure.Data.Sql.Companies.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DanskeBank.CompaniesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            Environment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DanskeBank.CompaniesApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"The correct authorization password is 'admin'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, GroupsCheckHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SsnService", policy =>
                    policy.Requirements.Add(new GroupsCheckRequirement("admin")));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddHostedService<Readiness>();
            services.AddSingleton<StartupHostedServiceHealthCheck>();

            services.AddHealthChecks()
                .AddCheck<HealthCheck>(
                    nameof(HealthCheck),
                    HealthStatus.Degraded,
                    tags: new[] { Tags.Health })
                .AddCheck<StartupHostedServiceHealthCheck>(
                    nameof(StartupHostedServiceHealthCheck),
                    HealthStatus.Degraded,
                    tags: new[] { Tags.Ready }); ;

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigurationModule(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DanskeBank.CompaniesApi v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains(Tags.Health),
                    ResponseWriter = (context, result) =>
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        return context.Response.WriteAsync(result.Status == HealthStatus.Healthy ? "GOOD" : "NOT GOOD");
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status404NotFound
                    }
                });

                endpoints.MapHealthChecks("/readiness", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains(Tags.Ready),
                    ResponseWriter = (context, result) =>
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        return context.Response.WriteAsync(result.Status == HealthStatus.Healthy ? "GOOD" : "NOT GOOD");
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "/");
            });

            var companiesContext = app.ApplicationServices.GetService<Context>();
            companiesContext.SeedData();
        }
    }
}
