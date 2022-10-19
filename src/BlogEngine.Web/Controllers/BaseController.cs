using BlogEngine.Core.Enums;
using BlogEngine.Core.Helpers;
using BlogEngine.Service.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogEngine.Web.Controllers;

public abstract class BaseController : Controller
{
    /// <summary>
    ///     Текущая роль
    /// </summary>
    protected UserRole Role => EnumMapper.MapUserRole(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value);

    /// <summary>
    ///     Аутентифицироваться
    /// </summary>
    protected async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(id));
    }
}
