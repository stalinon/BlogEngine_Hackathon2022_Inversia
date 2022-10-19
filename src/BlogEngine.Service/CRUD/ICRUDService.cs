using BlogEngine.Service.Models;

namespace BlogEngine.Service.CRUD;

/// <summary>
///     CRUD-сервис
/// </summary>
public interface ICRUDService<T> where T : BaseContract
{ 
    /// <summary>
    ///     Получить постранично
    /// </summary>
    public Task<PagedCollectionContract<T>> GetPagedCollectionAsync(PagedCollectionRequest request);

    /// <summary>
    ///     Получить по id
    /// </summary>
    public Task<T> GetByIdAsync(long id);

    /// <summary>
    ///     Получить все
    /// </summary>
    public Task<T[]> GetAllAsync();

    /// <summary>
    ///     Создать новый
    /// </summary>
    public Task<bool> CreateAsync(T item);

    /// <summary>
    ///     Обновить
    /// </summary>
    public Task<bool> UpdateAsync(T item);

    /// <summary>
    ///     Удалить по id
    /// </summary>
    public Task<bool> DeleteAsync(long id);
}
