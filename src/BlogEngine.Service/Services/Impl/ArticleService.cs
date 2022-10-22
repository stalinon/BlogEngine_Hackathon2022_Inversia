using BlogEngine.Service.Database.Entities;
using BlogEngine.Service.Models;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Service.Services.Impl;

/// <summary>
///     Сервис статей
/// </summary>
internal class ArticleService : CRUDServiceBase<ArticleContract, ArticleEntity>, ICRUDService<ArticleContract>
{
    /// <inheritdoc />
    public ArticleService(IUnitOfWork unitOfWork, IServiceScopeFactory mapper) : base(unitOfWork, mapper)
    { }

    /// <inheritdoc />
    protected override IQuery<ArticleEntity> MultipleResultQuery(IRepository<ArticleEntity> repository)
        => repository.MultipleResultQuery()
            .Include(s => s
                .Include(e => e.UserInfo)
                .Include(e => e.LeadingImage)
                .Include(e => e.Issue)
                .Include(e => e.Comments));

    /// <inheritdoc />
    protected override IQuery<ArticleEntity> SingleResultQuery(IRepository<ArticleEntity> repository)
        => repository.MultipleResultQuery()
            .Include(s => s
                .Include(e => e.UserInfo)
                .Include(e => e.LeadingImage)
                .Include(e => e.Issue)
                .Include(e => e.Comments));
}
