namespace BlogEngine.Service.Models;

/// <summary>
///     Вход пользователя
/// </summary>
public sealed class LoginContract
{
    /// <summary>
    ///     Никнейм
    /// </summary>
    public string Nickname { get; set; } = default!;

    /// <summary>
    ///     Пароль
    /// </summary>
    public string Password { get; set; } = default!;
}
