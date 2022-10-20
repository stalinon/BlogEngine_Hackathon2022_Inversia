using BlogEngine.Core.Enums;
using BlogEngine.Service.Attributes;
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
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> CreateAsync([FromBody] ArticleContract item)
        => await base.CreateAsync(item);

    /// <summary>
    ///     Обновить
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> UpdateAsync([FromBody] ArticleContract item)
        => await base.UpdateAsync(item);

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> DeleteAsync(long id)
        => await base.DeleteAsync(id);
}
