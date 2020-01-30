using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentApp.V1.Persistence.Contexts;

namespace StudentApp.Helpers.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString, string environmentName)
        {
            services.AddDbContext<StudentDbContext>(options =>
            {
                // If application is running in development mode, then use in memory database
                if (string.IsNullOrWhiteSpace(environmentName) || environmentName == Environments.Development)
                {
                    options.UseInMemoryDatabase("StudentApp").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
                else
                {
                    options.UseSqlServer(connectionString);
                }
            });

            return services;
        }
    }
}