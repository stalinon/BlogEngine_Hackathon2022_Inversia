using BlogEngine.Core.Enums;
using BlogEngine.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogEngine.Web.Controllers;

/// <inheritdoc cref="Controller"/>
public abstract class BaseController : Controller
{
    /// <summary>
    ///     Текущий пользователь
    /// </summary>
    protected string? Nickname => User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;

    /// <summary>
    ///     Текущая роль
    /// </summary>
    protected UserRole Role => EnumMapper.MapUserRole(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value);
}
