using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер статей
/// </summary>
[Route("api/articles")]
public class ArticleController : CRUDControllerBase<ArticleContract>
{
    /// <inheritdoc cref="ArticleController"/>
    public ArticleController(ICRUDService<ArticleContract> service) : base(service)
    { }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [HttpPost]
    public override async Task<IActionResult> CreateAsync(ArticleContract item)
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
    public override async Task<IActionResult> UpdateAsync(ArticleContract item)
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
