using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Base.Repository.Helpers.Configuracoes
{
    public static class Swagger
    {
        public static IServiceCollection ConfiguraSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                      new OpenApiInfo
                      {
                          Title = "Base",
                          Version = "v1",
                          Description = "Api do sistema Base - .Net Core 3.1",
                          Contact = new OpenApiContact
                          {
                              Name = "Jamsoft Sistemas",
                              Url = new Uri("http://www.jamsoft.com.br")
                          }
                      });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Entre com o token JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(
                 new OpenApiSecurityRequirement
                 {
                    {
                      new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                              Id = "Bearer",
                              Type = ReferenceType.SecurityScheme
                            }
                      },  new List<string>()
                    }
                 });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
