using BlogEngine.Service.Database.Entities;
using BlogEngine.Service.Models;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Service.Services.Impl;

/// <summary>
///     Сервис комментариев
/// </summary>
internal class CommentService : CRUDServiceBase<CommentContract, CommentEntity>, ICRUDService<CommentContract>
{
    /// <inheritdoc />
    public CommentService(IUnitOfWork unitOfWork, IServiceScopeFactory mapper) : base(unitOfWork, mapper)
    { }

    /// <inheritdoc />
    protected override IQuery<CommentEntity> MultipleResultQuery(IRepository<CommentEntity> repository)
        => repository.MultipleResultQuery().Include(s => s.Include(e => e.UserInfo).Include(e => e.Article));

    /// <inheritdoc />
    protected override IQuery<CommentEntity> SingleResultQuery(IRepository<CommentEntity> repository)
        => repository.SingleResultQuery().Include(s => s.Include(e => e.UserInfo).Include(e => e.Article));
}
