namespace BlogEngine.Service.Auth.Models;

/// <summary>
///     Регистрация пользователя
/// </summary>
public sealed class RegisterContract
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
