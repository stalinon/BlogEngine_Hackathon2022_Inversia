using System.Text.Json.Serialization;

namespace BlogEngine.Service.Models;

/// <summary>
///     Комментарий
/// </summary>
public sealed class CommentContract : BaseContract
{
    /// <summary>
    ///     Текст комментария
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    /// <summary>
    ///     Информация о пользователе
    /// </summary>
    [JsonPropertyName("user")]
    public UserContract Author { get; set; } = default!;
}
