using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Базовый контроллер с CRUD-операциями
/// </summary>
public abstract class CRUDControllerBase<T> : BaseController 
    where T : BaseContract
{
    protected ICRUDService<T> _service;

    /// <inheritdoc cref="CRUDControllerBase{T}"/>
    public CRUDControllerBase(ICRUDService<T> service) => _service = service;

    /// <summary>
    ///     Получить постранично
    /// </summary>
    [HttpGet("/pages")]
    public virtual async Task<IActionResult> GetPagedCollectionAsync([FromQuery] PagedCollectionRequest request)
    {
        var result = await _service.GetPagedCollectionAsync(request);
        return Ok(result);
    }

    /// <summary>
    ///     Получить по id
    /// </summary>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync(T item)
    {
        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    [HttpPut]
    public virtual async Task<IActionResult> UpdateAsync(T item)
    {
        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
