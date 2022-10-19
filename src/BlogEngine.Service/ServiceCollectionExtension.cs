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
        => services.AddDatabase(configuration)
                   .AddSingleton<IAuthService, AuthService>()
                   .AddSingleton<ICRUDService<UserContract>, UserService>()
                   .AddSingleton<ICRUDService<CommentContract>, CommentService>()
                   .AddSingleton<ICRUDService<ArticleContract>, ArticleService>()
                   .AddAutoMapper(typeof(MappingProfile));

    /// <summary>
    ///     Использовать сервис
    /// </summary>
    public static IApplicationBuilder UseService(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorHandlerMiddleware>();
}
