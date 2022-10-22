using BlogEngine.Core.Enums;
using BlogEngine.Service.Attributes;
using BlogEngine.Service.Models;
using BlogEngine.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

/// <summary>
///     Контроллер статей
/// </summary>
public class IssueController : CRUDControllerBase<IssueContract>
{
    /// <inheritdoc cref="IssueController"/>
    public IssueController(ICRUDService<IssueContract> service) : base(service)
    { }

    /// <summary>
    ///     Создать новый
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> CreateAsync([FromBody] IssueContract item)
        => await base.CreateAsync(item);

    /// <summary>
    ///     Обновить
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> UpdateAsync([FromBody] IssueContract item)
        => await base.UpdateAsync(item);

    /// <summary>
    ///     Удалить по id
    /// </summary>
    [Allowed(UserRole.ADMIN)]
    public override async Task<IActionResult> DeleteAsync(long id)
        => await base.DeleteAsync(id);
}
