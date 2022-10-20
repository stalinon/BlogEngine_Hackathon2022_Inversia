using BlogEngine.Service.Database;
using BlogEngine.Service.Mapping;
using BlogEngine.Service.Middlewares;
using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using BlogEngine.Service.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Service;

/// <summary>
///     Расширение для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///     Добавить сервис
    /// </summary>
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<ICRUDService<UserContract>, UserService>();
        services.AddSingleton<ICRUDService<CommentContract>, CommentService>();
        services.AddSingleton<ICRUDService<ArticleContract>, ArticleService>();
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    /// <summary>
    ///     Использовать сервис
    /// </summary>
    public static IApplicationBuilder UseService(this IApplicationBuilder builder, IServiceProvider provider)
    {
        builder.UseMiddleware<ErrorHandlerMiddleware>();
        provider.ApplyMigrations();
        provider.CreateAdmin();

        return builder;
    }
}
