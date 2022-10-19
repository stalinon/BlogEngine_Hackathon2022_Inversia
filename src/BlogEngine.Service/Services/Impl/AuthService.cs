using AutoMapper;
using BlogEngine.Service.Models;
using BlogEngine.Service.Database.Entities;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlogEngine.Service.Services.Impl;

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
    public async Task<bool> LoginAsync(LoginContract loginContract, HttpContext context, CancellationToken cancellationToken = default)
    {
        using var repository = _unitOfWork.Repository<UserEntity>();

        var query = repository
            .SingleResultQuery()
            .Include(s => s.Include(q => q.UserInfo))
            .AndFilter(u => u.UserInfo.Nickname == loginContract.Nickname);

        var entity = await repository.FirstOrDefaultAsync(query, cancellationToken);

        if (entity != null && BCrypt.Net.BCrypt.Verify(loginContract.Password, entity.PasswordHash))
        {
            await Authenticate(context, entity);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<bool> RegisterAsync(RegisterContract registerContract, HttpContext context, CancellationToken cancellationToken = default)
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
            await Authenticate(context, entity);
            return true;
        }

        return false;
    }

    private static async Task Authenticate(HttpContext context, UserEntity entity)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, entity.UserInfo.Nickname.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, entity.Role.ToString()),
            };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}
