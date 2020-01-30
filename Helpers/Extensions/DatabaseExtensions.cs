using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentApp.V1.Persistence.Contexts;

namespace StudentApp.Helpers.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // If application is running in development mode, then use in memory database
            if (env.IsDevelopment())
            {
                services.AddDbContext<StudentDbContext>(options =>
                    options
                    .UseInMemoryDatabase("StudentApp")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            }
            else
            {
                services.AddDbContext<StudentDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")));
            }

            return services;
        }
    }
}