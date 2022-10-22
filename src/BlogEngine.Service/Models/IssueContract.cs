using System.Text.Json.Serialization;

namespace BlogEngine.Service.Models;

/// <summary>
///     Контракт выпуска журнала
/// </summary>
public sealed class IssueContract : BaseContract
{
    /// <summary>
    ///     Номер выпуска
    /// </summary>
    [JsonPropertyName("issue_number")]
    public long IssueNumber { get; set; }

    /// <summary>
    ///     Дата выпуска
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    /// <summary>
    ///     Ведущее изображение
    /// </summary>
    [JsonPropertyName("leading_image")]
    public string? LeadingImage { get; set; }

    /// <summary>
    ///     Статьи
    /// </summary>
    [JsonPropertyName("article_ids")]
    public long[] ArticleIds { get; set; } = Array.Empty<long>();
}
