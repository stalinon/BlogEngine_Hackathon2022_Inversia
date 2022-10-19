namespace BlogEngine.Service.Models;

/// <summary>
///     Запрос пагинированной коллекции
/// </summary>
public sealed class PagedCollectionRequest
{
    /// <summary>
    ///     Идентификатор начала страницы
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    ///     Количество элементов на странице
    /// </summary>
    public int Capacity { get; set; }
}
