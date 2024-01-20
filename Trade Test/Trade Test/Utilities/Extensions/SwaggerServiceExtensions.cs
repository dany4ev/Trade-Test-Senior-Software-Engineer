using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerUI;

namespace Trade_Test.Utilities.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, 
            string apiTitle, string apiVersion, string apiXmlPath)
        {
            services.AddSwaggerGen(options =>
            {
                // c.CustomSchemaIds(y => y.FullName);
                options.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });

                options.IncludeXmlComments(apiXmlPath);

                //First we define the security scheme
                options.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme // Key
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", // Note: The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>() // Value
                    }
                });
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, string swaggerJsonName, string apiTitle)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);

                c.SwaggerEndpoint(swaggerJsonName, apiTitle);
                //c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
