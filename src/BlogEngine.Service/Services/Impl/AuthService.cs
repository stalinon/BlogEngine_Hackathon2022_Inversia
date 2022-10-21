using AutoMapper;
using BlogEngine.Service.Models;
using BlogEngine.Service.Database.Entities;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogEngine.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;
using BlogEngine.Service.Configuration;
using System.IdentityModel.Tokens.Jwt;

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
    public async Task<string?> LoginAsync(LoginContract loginContract, HttpContext context, CancellationToken cancellationToken = default)
    {
        using var repository = _unitOfWork.Repository<UserEntity>();

        var query = repository
            .SingleResultQuery()
            .Include(s => s.Include(q => q.UserInfo))
            .AndFilter(u => u.UserInfo.Nickname == loginContract.Nickname);

        var entity = await repository.FirstOrDefaultAsync(query, cancellationToken);

        return entity != null && BCrypt.Net.BCrypt.Verify(loginContract.Password, entity.PasswordHash) ? Authenticate(entity) : null;
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
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<bool> ExitAsync(HttpContext context, CancellationToken cancellationToken = default) 
    {
        return true;
    }

    /// <inheritdoc />
    public async Task<UserContract> GetMeAsync(HttpContext context, CancellationToken cancellationToken = default)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var auth))
        {
            throw new AppException("Not authorized", System.Net.HttpStatusCode.Unauthorized);
        }

        var token = new JwtSecurityTokenHandler().ReadJwtToken(auth.FirstOrDefault(x => x.StartsWith("Bearer"))?.Replace("Bearer ", ""));
        
        var nickname = token.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
        using var repository = _unitOfWork.Repository<UserEntity>();

        var query = repository
            .SingleResultQuery()
            .Include(s => s.Include(q => q.UserInfo))
            .AndFilter(u => u.UserInfo.Nickname == nickname);

        var entity = await repository.FirstOrDefaultAsync(query, cancellationToken);

        return entity == null
            ? throw new AppException("Account not found", System.Net.HttpStatusCode.NotFound)
            : _mapper.Map<UserContract>(entity);
    }

    private static string Authenticate(UserEntity entity)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, entity.UserInfo.Nickname),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, entity.Role.ToString()),
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
