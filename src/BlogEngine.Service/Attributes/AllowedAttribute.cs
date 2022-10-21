using BlogEngine.Core.Enums;
using BlogEngine.Core.Helpers;
using BlogEngine.Service.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogEngine.Service.Attributes;

/// <summary>
///     Атрибут для авторизации
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowedAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    ///     Разрешенные роли
    /// </summary>
    public UserRole[] Roles { get; set; }

    /// <inheritdoc cref="AllowedAttribute"/>
    public AllowedAttribute(params UserRole[] roles) => Roles = roles ?? Array.Empty<UserRole>();

    /// <summary>
    ///     Разрешен ли доступ
    /// </summary>
    public bool IsAllowed(UserRole role) 
        => Roles.Contains(role) 
        ? true 
        : throw new AppException("Forbidden", System.Net.HttpStatusCode.Forbidden);
    public void OnAuthorization(AuthorizationFilterContext context) 
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var auth))
        {
            throw new AppException("Not authorized", System.Net.HttpStatusCode.Unauthorized);
        }

        var token = new JwtSecurityTokenHandler().ReadJwtToken(auth.FirstOrDefault(x => x.StartsWith("Bearer"))?.Replace("Bearer ", ""));
        var claim = token.Claims?
                            .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);
        var role = EnumMapper.MapUserRole(claim?.Value);

        IsAllowed(role);
    }
}
