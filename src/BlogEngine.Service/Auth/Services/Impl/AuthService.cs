using AutoMapper;
using BlogEngine.Service.Auth.Models;
using BlogEngine.Service.Database.Entities;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Service.Auth.Services.Impl;

/// <inheritdoc cref="IAuthService"/>
internal sealed class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <inheritdoc cref="AuthService"/>
    public AuthService(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<bool> LoginAsync(LoginContract loginContract, CancellationToken cancellationToken = default)
    {
        using var repository = _unitOfWork.Repository<UserEntity>();

        var query = repository
            .SingleResultQuery()
            .Include(s => s.Include(q => q.UserInfo))
            .AndFilter(u => u.UserInfo.Nickname == loginContract.Nickname);

        var entity = await repository.FirstOrDefaultAsync(query, cancellationToken);

        return entity != null && BCrypt.Net.BCrypt.Verify(loginContract.Password, entity.PasswordHash);
    }

    /// <inheritdoc />
    public async Task<bool> RegisterAsync(RegisterContract registerContract, CancellationToken cancellationToken = default)
    {
        using var repository = _unitOfWork.Repository<UserEntity>();

        var query = repository
            .SingleResultQuery()
            .Include(s => s.Include(q => q.UserInfo))
            .AndFilter(u => u.UserInfo.Nickname == registerContract.Nickname);

        var entity = await repository.FirstOrDefaultAsync(query, cancellationToken);

        if (entity == null)
        {
            registerContract.Password = BCrypt.Net.BCrypt.HashPassword(registerContract.Password);
            entity = _mapper.Map<UserEntity>(registerContract);
            await repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }

        return false;
    }
}
