using System.Text.Json.Serialization;

namespace BlogEngine.Service.Models;

/// <summary>
///     Базовый контракт
/// </summary>
public class BaseContract
{
    /// <summary>
    ///     ID
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Обновлен
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    ///     Создан
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }
}
