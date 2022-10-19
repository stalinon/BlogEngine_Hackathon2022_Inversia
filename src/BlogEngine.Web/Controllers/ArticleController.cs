using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер статей
/// </summary>
public class ArticleController : CRUDControllerBase<ArticleContract>
{
    /// <inheritdoc cref="ArticleController"/>
    public ArticleController(ICRUDService<ArticleContract> service) : base(service)
    { }

    /// <summary>
    ///     Создать новый
    /// </summary>
    public override async Task<IActionResult> CreateAsync([FromBody] ArticleContract item)
    {
        CheckRole(Core.Enums.UserRole.ADMIN);

        var result = await _service.CreateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    public override async Task<IActionResult> UpdateAsync([FromBody] ArticleContract item)
    {
        CheckRole(Core.Enums.UserRole.ADMIN);

        var result = await _service.UpdateAsync(item);
        return Ok();
    }

    /// <summary>
    ///     Удалить по id
    /// </summary>
    public override async Task<IActionResult> DeleteAsync(long id)
    {
        CheckRole(Core.Enums.UserRole.ADMIN);

        var result = await _service.DeleteAsync(id);
        return Ok();
    }
}
