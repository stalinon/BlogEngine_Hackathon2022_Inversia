using System.Text.Json.Serialization;

namespace BlogEngine.Service.Models;

public sealed class ArticleContract : BaseContract
{
    /// <summary>
    ///     Заголовок статьи
    /// </summary>
    [JsonPropertyName("header")]
    public string Header { get; set; } = default!;

    /// <summary>
    ///     Описание статьи
    /// </summary>
    [JsonPropertyName("desc")]
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Ведущее изображение
    /// </summary>
    [JsonPropertyName("leading_image")]
    public string? LeadingImage { get; set; }

    /// <summary>
    ///     Информация о пользователе
    /// </summary>

    [JsonPropertyName("author")]
    public UserContract Author { get; set; } = default!;

    /// <summary>
    ///     Текст статьи, размеченный HTML
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    /// <summary>
    ///     Комментарии к статье
    /// </summary>
    public CommentContract[]? Comments { get; set; }
}