using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilySync.Core.Persistence.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMySQLContext<TContext>(this IServiceCollection services, string dbName, IConfiguration config) where TContext : DbContext
    {
        var connectionString = config.GetConnectionString("MySQL");
        var version = new MySqlServerVersion("8.0.26");

        services.AddDbContext<DbContext, TContext>(options =>
        {
            options.UseMySql($"{connectionString};Database={dbName}", version, options =>
            {
                options.EnableRetryOnFailure();
            });
        });

        return services;
    }
}