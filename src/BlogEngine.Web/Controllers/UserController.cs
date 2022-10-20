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
        => await base.GetPagedCollectionAsync(request);

    /// <summary>
    ///     Получить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> GetByIdAsync(long id)
        => await base.GetByIdAsync(id);

    /// <summary>
    ///     Получить все
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> GetAllAsync()
        => await base.GetAllAsync();

    /// <summary>
    ///     Создать новый
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> CreateAsync([FromBody] UserContract item)
        => await base.CreateAsync(item);

    /// <summary>
    ///     Обновить
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> UpdateAsync([FromBody] UserContract item)
        => await base.UpdateAsync(item);

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> DeleteAsync(long id)
        => await base.DeleteAsync(id);
}
