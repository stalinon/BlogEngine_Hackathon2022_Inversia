using AutoMapper;
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICRUDService<UserContract>, UserService>();
        services.AddScoped<ICRUDService<CommentContract>, CommentService>();
        services.AddScoped<ICRUDService<ArticleContract>, ArticleService>();
        services.AddScoped<ICRUDService<IssueContract>, IssueService>();

        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
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
