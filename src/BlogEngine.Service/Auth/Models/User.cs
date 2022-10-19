using BlogEngine.Core.Enums;

namespace BlogEngine.Service.Auth.Models;

/// <summary>
///     Аккаунт пользователя
/// </summary>
public sealed class User
{
    /// <summary>
    ///     Идентификатор пользователя в БД
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Никнейм
    /// </summary>
    public string Nickname { get; set; } = default!;

    /// <summary>
    ///     Роль пользователя
    /// </summary>

    public UserRole Role { get; set; }
}
