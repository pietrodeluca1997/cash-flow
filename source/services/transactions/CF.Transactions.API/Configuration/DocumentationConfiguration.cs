using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CF.Transactions.API.Configuration
{
    public static class DocumentationConfiguration
    {
        /// <summary>
        ///     Add swagger
        /// </summary>
        /// <param name="services">Extension method</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cash Flow Transactions API",
                    Description = "An API responsible for all functionality related to account transactions.",
                    Contact = new OpenApiContact
                    {
                        Name = "Pietro Romano",
                        Email = "pietro.romano.tech@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/pietro-de-luca-2761aa18b/")
                    }
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swaggerOptions.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        ///     Use swagger
        /// </summary>
        /// <param name="appBuilder">Extension method</param>
        public static void UseSwaggerDocumentation(this IApplicationBuilder appBuilder)
        {
            string swaggerEndpoint = "/swagger/v1/swagger.json";

            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(swaggerOptions =>
            {
                swaggerOptions.SwaggerEndpoint(swaggerEndpoint, "CF.Transactions.API v1");
                swaggerOptions.RoutePrefix = string.Empty;
                //swaggerOptions.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
