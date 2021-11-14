﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace FinancialManager.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException($"{nameof(services)} is required for configure swagger.");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinancialManager.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                                {
                                    In = ParameterLocation.Header,
                                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.ApiKey
                                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                c.EnableAnnotations();
            });

            return services;
        }

        internal static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException($"{nameof(app)} is required for configure swagger middleware.");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancialManager.API v1"));

            return app;
        }
    }
}
