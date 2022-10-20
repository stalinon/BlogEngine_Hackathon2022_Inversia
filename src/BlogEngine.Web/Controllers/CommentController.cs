using BlogEngine.Core.Enums;
using BlogEngine.Service.Attributes;
using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер комментариев
/// </summary>
public class CommentController : CRUDControllerBase<CommentContract>
{
    /// <inheritdoc cref="CommentController"/>
    public CommentController(ICRUDService<CommentContract> service) : base(service)
    { }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [Allowed(UserRole.USER)]
    public override async Task<IActionResult> CreateAsync([FromBody] CommentContract item)
    {
        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> UpdateAsync([FromBody] CommentContract item)
    {
        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> DeleteAsync([FromBody] long id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
