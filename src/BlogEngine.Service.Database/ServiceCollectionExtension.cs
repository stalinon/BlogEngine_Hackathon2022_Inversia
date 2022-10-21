using BlogEngine.Core;
using BlogEngine.Core.Enums;
using BlogEngine.Service.Database.Entities;
using EntityFrameworkCore.UnitOfWork.Extensions;
using EntityFrameworkCore.UnitOfWork.Interfaces;
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
            => option.UseNpgsql(dbConnectionString, o => o.MigrationsHistoryTable("MIGRATIONS")), ServiceLifetime.Scoped);
        services.AddScoped<DbContext, DatabaseContext>();
        services.AddUnitOfWork(ServiceLifetime.Scoped);
        services.AddUnitOfWork<DatabaseContext>(ServiceLifetime.Scoped);

        return services;
    }

    /// <summary>
    ///     Применяет миграции
    /// </summary>
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            db.Database.SetCommandTimeout(TimeSpan.FromDays(2));
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception("Error applying database migrations", ex);
        }
    }

    /// <summary>
    ///     Создать аккаунт администратора
    /// </summary>
    public static IServiceProvider CreateAdmin(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var uof = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var users = uof.Repository<UserEntity>();

        if (!users.Any())
        {
            var login = Environment.GetEnvironmentVariable(ConfigKeys.ADMIN_LOGIN) ?? ConfigDefaults.ADMIN_LOGIN;
            var password = BCrypt.Net.BCrypt.HashPassword(Environment.GetEnvironmentVariable(ConfigKeys.ADMIN_PWD) ?? ConfigDefaults.ADMIN_PWD);
            var admin = Create(login, password);

            users.Add(admin);
            uof.SaveChanges();
        }

        return serviceProvider;

        static UserEntity Create(string login, string password)
        {
            var adminInfo = new UserInfoEntity()
            {
                Id = 1,
                Nickname = login,
                FirstName = login,
                LastName = login,
            };

            var admin = new UserEntity
            {
                Id = 1,
                Role = UserRole.ADMIN,
                PasswordHash = password,
                UserInfo = adminInfo
            };
            return admin;
        }
    }
}
