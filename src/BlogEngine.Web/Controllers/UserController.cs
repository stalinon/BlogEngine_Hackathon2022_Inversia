using BlogEngine.Core.Enums;
using BlogEngine.Service.Attributes;
using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер пользователей
/// </summary>
public sealed class UserController : CRUDControllerBase<UserContract>
{
    /// <inheritdoc cref="UserController"/>
    public UserController(ICRUDService<UserContract> service) : base(service)
    { }

    /// <summary>
    ///     Получить постранично
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> GetPagedCollectionAsync([FromQuery] PagedCollectionRequest request)
    {
        var result = await _service.GetPagedCollectionAsync(request);
        return Ok(result);
    }

    /// <summary>
    ///     Получить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> CreateAsync([FromBody] UserContract item)
    {
        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> UpdateAsync([FromBody] UserContract item)
    {
        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
