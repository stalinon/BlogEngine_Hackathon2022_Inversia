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
    [HttpPost("api/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterContract item)
    {
        var result = await _service.RegisterAsync(item, HttpContext);
        return Ok();
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost("api/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginContract item)
    {
        var result = await _service.LoginAsync(item, HttpContext);
        return result ? Ok() : Unauthorized();
    }

    /// <summary>
    ///     Выйти
    /// </summary>
    [HttpPost("api/exit")]
    public async Task<IActionResult> ExitAsync()
    {
        var result = await _service.ExitAsync(HttpContext);
        return Ok();
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpGet("api/me")]
    public async Task<IActionResult> MeAsync()
    {
        var result = await _service.GetMeAsync(HttpContext);
        return Ok(result);
    }
}
