using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер комментариев
/// </summary>
[Route("api/comments")]
public class CommentController : CRUDControllerBase<CommentContract>
{
    /// <inheritdoc cref="CommentController"/>
    public CommentController(ICRUDService<CommentContract> service) : base(service)
    { }


    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost]
    public override async Task<IActionResult> CreateAsync(CommentContract item)
    {
        if (!CheckRole(Core.Enums.UserRole.USER))
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
    public override async Task<IActionResult> UpdateAsync(CommentContract item)
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
