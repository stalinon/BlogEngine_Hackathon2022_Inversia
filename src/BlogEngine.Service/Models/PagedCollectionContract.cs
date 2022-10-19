namespace BlogEngine.Service.Models;

/// <summary>
///     Контракт с коллекцией постранично
/// </summary>
public class PagedCollectionContract<T>
{
    /// <summary>
    ///     Всего элементов
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    ///     Элементов в коллекции
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    ///     Коллекция
    /// </summary>
    public T[] Items { get; set; } = Array.Empty<T>();

    /// <summary>
    ///     Текущая страница
    /// </summary>
    public int? Page { get; set; }

}
