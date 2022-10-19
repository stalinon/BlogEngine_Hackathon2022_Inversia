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
    public Task<bool> LoginAsync(LoginContract loginContract, HttpContext context, CancellationToken cancellationToken = default);
}
