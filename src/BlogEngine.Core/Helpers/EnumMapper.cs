using BlogEngine.Core.Enums;

namespace BlogEngine.Core.Helpers;

/// <summary>
///     Маппер перечислений
/// </summary>
public static class EnumMapper
{
    /// <summary>
    ///     Получить роль пользователя
    /// </summary>
    public static UserRole MapUserRole(string? role)
        => role switch
        {
            "USER" => UserRole.USER,
            "ADMIN" => UserRole.ADMIN,
            _ => UserRole.UNAUTHORIZED,
        };
}
