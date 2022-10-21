using AutoMapper;
using BlogEngine.Service.Database.Entities;
using BlogEngine.Service.Exceptions;
using BlogEngine.Service.Models;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Service.Services.Impl;

/// <inheritdoc cref="ICRUDService{TContract}"/>
public abstract class CRUDServiceBase<TContract, TEntity> : ICRUDService<TContract>
    where TContract : BaseContract
    where TEntity : BaseEntity, IHasEntityId
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <inheritdoc cref="CRUDServiceBase{TContract, TEntity}"/>
    public CRUDServiceBase(IUnitOfWork unitOfWork, IServiceScopeFactory factory)
    {
        _unitOfWork = unitOfWork;
        var scope = factory.CreateScope();
        _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    }

    /// <inheritdoc />
    public async Task<bool> CreateAsync(TContract item)
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var entity = _mapper.Map<TEntity>(item);
        await repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(long id)
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var exists = await repository.AnyAsync(x => x.Id == id);

        if (!exists)
        {
            throw new AppException("Entity not found", System.Net.HttpStatusCode.NotFound);
        }

        await repository.RemoveAsync(x => x.Id == id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<TContract[]> GetAllAsync() 
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var entities = await repository.SearchAsync(MultipleResultQuery(repository));
        return _mapper.Map<TContract[]>(entities.ToArray());

    }

    /// <inheritdoc />
    public async Task<TContract> GetByIdAsync(long id)
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var entity = await repository.SingleOrDefaultAsync(SingleResultQuery(repository).AndFilter(x => x.Id == id));
        return _mapper.Map<TContract>(entity);
    }

    /// <inheritdoc />
    public async Task<PagedCollectionContract<TContract>> GetPagedCollectionAsync(PagedCollectionRequest request)
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var query = ((IMultipleResultQuery<TEntity>)MultipleResultQuery(repository)).Page(request.Page, request.Capacity);
        var entities = await repository.SearchAsync(query);
        return new PagedCollectionContract<TContract>
        {
            TotalCount = query.Paging.TotalCount,
            Page = query.Paging.PageIndex,
            Count = entities.Count,
            Items = _mapper.Map<TContract[]>(entities.ToArray())
        };
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(TContract item) 
    {
        using var repository = _unitOfWork.Repository<TEntity>();
        var exists = await repository.AnyAsync(x => x.Id == item.Id);

        if (!exists)
        {
            throw new AppException("Entity not found", System.Net.HttpStatusCode.NotFound);
        }

        await repository.UpdateAsync(x => x.Id == item.Id, t => _mapper.Map<TEntity>(item));
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <summary>
    ///     Одиночный запрос 
    /// </summary>
    protected abstract IQuery<TEntity> SingleResultQuery(IRepository<TEntity> repository);

    /// <summary>
    ///     Одиночный запрос 
    /// </summary>
    protected abstract IQuery<TEntity> MultipleResultQuery(IRepository<TEntity> repository);
}
