using Common.Domain.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Implementations.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services, SwaggerOptions swaggerOptions, OAuthOptions oAuthOptions, string pathToXMLDocument)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo { Title = swaggerOptions.Title, Version = swaggerOptions.Version });

                options.IncludeXmlComments(pathToXMLDocument);

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Name = "oauth2",
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(string.Concat(oAuthOptions.AuthServer, oAuthOptions.TokenPath)),
                            Scopes = swaggerOptions.SwaggerScopes
                        }
                    }
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }

                        }, swaggerOptions.SwaggerScopes.Keys.ToArray()
                    }
                });


            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app, SwaggerOptions swaggerOptions, OAuthOptions oAuthOptions)
        {

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
                options.RoutePrefix = "swagger";
                options.OAuthClientSecret("secret");
                options.OAuthClientId("Swagger");
                options.OAuthAppName(oAuthOptions.ApiResourse);
            });


        }
    }


}
