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
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetValue<string>(ConfigKeys.DB_CONNECTION_STRING);

        services.AddDbContext<DatabaseContext>(option => option.UseNpgsql(dbConnectionString));
        services.AddScoped<DbContext, DatabaseContext>();
        services.AddUnitOfWork<DatabaseContext>();
    }

}
