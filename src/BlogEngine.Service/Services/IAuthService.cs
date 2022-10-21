using BlogEngine.Service.Models;
using Microsoft.AspNetCore.Http;

namespace BlogEngine.Service.Services;

/// <summary>
///     Авторизация
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Зарегистрироваться
    /// </summary>
    public Task<bool> RegisterAsync(RegisterContract registerContract, HttpContext context, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Залогиниться
    /// </summary>
    public Task<string?> LoginAsync(LoginContract loginContract, HttpContext context, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Выйти
    /// </summary>
    public Task<bool> ExitAsync(HttpContext context, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Получить мой аккаунт
    /// </summary>
    public Task<UserContract> GetMeAsync(HttpContext context, CancellationToken cancellationToken = default);
}
