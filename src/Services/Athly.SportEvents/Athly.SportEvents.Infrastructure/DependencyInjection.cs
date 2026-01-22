using Athly.SportEvents.Application.Interfaces;
using Athly.SportEvents.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Athly.SportEvents.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AthlySportEventsDB");

            services.AddDbContext<SportEventsContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.UseNetTopologySuite();
                    sqlOptions.MigrationsAssembly(typeof(SportEventsContext).Assembly.FullName);

                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));

            services.AddScoped<ISportEventsContext>(provider =>
            provider.GetRequiredService<SportEventsContext>());

            return services;
        }
    }
}
