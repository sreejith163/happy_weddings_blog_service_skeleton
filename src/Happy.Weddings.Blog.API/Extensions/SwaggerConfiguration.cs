using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Happy.Weddings.Blog.API.Extensions
{
    /// <summary>
    /// Extension for adding swagger documentation
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Creates the response compression.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    configuration["Version"],
                    new OpenApiInfo { Title = configuration["Title"], Version = configuration["Version"] });
                c.IncludeXmlComments(@"App_Data\api-comments.xml");
            });

            return services;
        }
    }
}
