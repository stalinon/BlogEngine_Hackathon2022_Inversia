namespace BlogEngine.Service.Models;

/// <summary>
///     Регистрация пользователя
/// </summary>
public sealed class RegisterContract
{
    /// <summary>
    ///     Никнейм
    /// </summary>
    public string Nickname { get; set; } = default!;

    /// <summary>
    ///     Имя
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    ///     Фамилия
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    ///     Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    ///     Фото в Base64
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    ///     Оригинальная ширина изображения
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    ///     Оригинальная высота изображения
    /// </summary>
    public int Height { get; set; }
}
