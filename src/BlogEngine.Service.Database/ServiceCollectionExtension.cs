using BlogEngine.Core;
using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Service.Database;

/// <summary>
///     Расширение для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///     Добавить поддержку БД
    /// </summary>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetValue<string>(ConfigKeys.DB_CONNECTION_STRING, ConfigDefaults.DB_CONNECTION_STRING);

        services.AddDbContext<DatabaseContext>(option 
            => option.UseNpgsql(dbConnectionString, o => o.MigrationsHistoryTable("MIGRATIONS")), ServiceLifetime.Singleton);
        services.AddSingleton<DbContext, DatabaseContext>();
        services.AddUnitOfWork(ServiceLifetime.Singleton);
        services.AddUnitOfWork<DatabaseContext>(ServiceLifetime.Singleton);

        services.BuildServiceProvider().ApplyMigrations<DatabaseContext>();

        return services;
    }

    /// <summary>
    ///     Применяет миграции
    /// </summary>
    public static void ApplyMigrations<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<T>();

            db.Database.SetCommandTimeout(TimeSpan.FromDays(2));
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception("Error applying database migrations", ex);
        }
    }

}
