using Microsoft.Extensions.DependencyInjection;

namespace Happy.Weddings.Blog.API.Extensions
{
    /// <summary>
    /// Extension for adding health checks
    /// </summary>
    public static class HealthCheck
    {
        /// <summary>
        /// Adds the health checks.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
    }
}
