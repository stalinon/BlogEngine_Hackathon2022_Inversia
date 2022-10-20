using BlogEngine.Core.Enums;
using System.Text.Json.Serialization;

namespace BlogEngine.Service.Models;

/// <summary>
///     Пользователь
/// </summary>
public sealed class UserContract : BaseContract
{
    /// <summary>
    ///     Роль
    /// </summary>
    [JsonPropertyName("role")]
    public UserRole Role { get; set; }

    /// <summary>
    ///     Никнейм пользователя
    /// </summary>
    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = default!;

    /// <summary>
    ///     Имя
    /// </summary>
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = default!;

    /// <summary>
    ///     Фамилия
    /// </summary>
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = default!;

    /// <summary>
    ///     Изображение профиля
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
