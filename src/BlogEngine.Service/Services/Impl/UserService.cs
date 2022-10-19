using AutoMapper;
using BlogEngine.Service.Database.Entities;
using BlogEngine.Service.Models;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Service.Services.Impl;

/// <summary>
///     Сервис пользователей
/// </summary>
internal class UserService : CRUDServiceBase<UserContract, UserEntity>, ICRUDService<UserContract>
{
    /// <inheritdoc cref="UserService"/>
    public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    { }

    /// <inheritdoc />
    protected override IQuery<UserEntity> MultipleResultQuery(IRepository<UserEntity> repository)
        => repository.MultipleResultQuery().Include(s => s.Include(e => e.UserInfo));

    /// <inheritdoc />
    protected override IQuery<UserEntity> SingleResultQuery(IRepository<UserEntity> repository)
        => repository.SingleResultQuery().Include(s => s.Include(e => e.UserInfo));
}
