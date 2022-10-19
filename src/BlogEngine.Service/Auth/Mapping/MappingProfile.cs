using AutoMapper;
using BlogEngine.Core.Enums;
using BlogEngine.Service.Auth.Models;
using BlogEngine.Service.Database.Entities;

namespace BlogEngine.Service.Auth.Mapping;

/// <summary>
///     Профиль маппинга для авторизации
/// </summary>
internal sealed class MappingProfile : Profile
{
    /// <inheritdoc />
    public MappingProfile() => CreateMap<RegisterContract, UserEntity>()
            .ForMember(d => d.PasswordHash, s => s.MapFrom(m => m.Password))
            .ForMember(d => d.Role, s => s.MapFrom(m => UserRole.USER))
            .ForMember(d => d.UserInfo.Nickname, s => s.MapFrom(m => m.Nickname))
            .ForMember(d => d.UserInfo.FirstName, s => s.MapFrom(m => m.FirstName))
            .ForMember(d => d.UserInfo.LastName, s => s.MapFrom(m => m.LastName))
            .ForMember(d => d.UserInfo.Image.Base64, s => s.MapFrom(m => m.Image))
            .ForMember(d => d.UserInfo.Image.Width, s => s.MapFrom(m => m.Width))
            .ForMember(d => d.UserInfo.Image.Height, s => s.MapFrom(m => m.Height));
}
