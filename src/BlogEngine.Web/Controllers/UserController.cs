using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер пользователей
/// </summary>
[Route("api/users")]
public sealed class UserController : CRUDControllerBase<UserContract>
{
    /// <inheritdoc cref="UserController"/>
    public UserController(ICRUDService<UserContract> service) : base(service)
    { }

    /// <summary>
    ///     Получить постранично
    /// </summary>
    [HttpGet("/pages")]
    public override async Task<IActionResult> GetPagedCollectionAsync([FromQuery] PagedCollectionRequest request)
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.GetPagedCollectionAsync(request);
        return Ok(result);
    }

    /// <summary>
    ///     Получить по id
    /// </summary>
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetByIdAsync(long id)
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все
    /// </summary>
    [HttpGet]
    public override async Task<IActionResult> GetAllAsync()
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost]
    public override async Task<IActionResult> CreateAsync(UserContract item)
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    [HttpPut]
    public override async Task<IActionResult> UpdateAsync(UserContract item)
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [HttpDelete("{id}")]
    public override async Task<IActionResult> DeleteAsync(long id)
    {
        if (!CheckRole(Core.Enums.UserRole.ADMIN))
        {
            return Forbid();
        }

        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
