using BlogEngine.Core.Enums;
using BlogEngine.Service.Attributes;
using BlogEngine.Service.Exceptions;
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
    [Allowed(UserRole.ADMIN, UserRole.USER)]
    public override async Task<IActionResult> UpdateAsync([FromBody] CommentContract item)
    {
        if (item.Author.Nickname != Nickname)
        {
            throw new AppException("You're not allowed to edit this comment", System.Net.HttpStatusCode.Forbidden);
        }

        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN, UserRole.USER)]
    public override async Task<IActionResult> DeleteAsync([FromBody] long id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item.Author.Nickname != Nickname && Role != UserRole.ADMIN)
        {
            throw new AppException("You're not allowed to delete this comment", System.Net.HttpStatusCode.Forbidden);
        }

        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
