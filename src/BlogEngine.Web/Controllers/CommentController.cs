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
    public override async Task<IActionResult> CreateAsync([FromBody] CommentContract item)
    {
        CheckRole(Core.Enums.UserRole.USER);

        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    public override async Task<IActionResult> UpdateAsync([FromBody] CommentContract item)
    {
        CheckRole(Core.Enums.UserRole.ADMIN);

        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    public override async Task<IActionResult> DeleteAsync([FromBody] long id)
    {
        CheckRole(Core.Enums.UserRole.ADMIN);

        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
