using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер авторизации
/// </summary>
public class AuthController : BaseController
{
    private IAuthService _service;

    /// <inheritdoc cref="AuthController"/>
    public AuthController(IAuthService service) => _service = service;

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost("/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterContract item)
    {
        var result = await _service.RegisterAsync(item, HttpContext);
        return Ok();
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginContract item)
    {
        var result = await _service.LoginAsync(item, HttpContext);

        return Ok();
    }
}
