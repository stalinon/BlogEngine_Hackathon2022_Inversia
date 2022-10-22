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
internal class IssueService : CRUDServiceBase<IssueContract, IssueEntity>, ICRUDService<IssueContract>
{
    /// <inheritdoc />
    public IssueService(IUnitOfWork unitOfWork, IServiceScopeFactory mapper) : base(unitOfWork, mapper)
    { }

    /// <inheritdoc />
    protected override IQuery<IssueEntity> MultipleResultQuery(IRepository<IssueEntity> repository)
        => repository.MultipleResultQuery().Include(s => s.Include(e => e.Articles).Include(e => e.LeadingImage));

    /// <inheritdoc />
    protected override IQuery<IssueEntity> SingleResultQuery(IRepository<IssueEntity> repository)
        => repository.MultipleResultQuery().Include(s => s.Include(e => e.Articles).Include(e => e.LeadingImage));
}
