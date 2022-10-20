using AutoMapper;
using BlogEngine.Core.Enums;
using BlogEngine.Service.Database.Entities;
using BlogEngine.Service.Models;

namespace BlogEngine.Service.Mapping;

/// <summary>
///     Профиль для маппинга
/// </summary>
internal class MappingProfile : Profile
{
    /// <inheritdoc />
    public MappingProfile()
    {
        CreateMap<RegisterContract, UserEntity>()
            .ForMember(d => d.PasswordHash, s => s.MapFrom(m => m.Password))
            .ForMember(d => d.Role, s => s.MapFrom(m => UserRole.USER))
            .ForPath(d => d.UserInfo.Nickname, s => s.MapFrom(m => m.Nickname))
            .ForPath(d => d.UserInfo.FirstName, s => s.MapFrom(m => m.FirstName))
            .ForPath(d => d.UserInfo.LastName, s => s.MapFrom(m => m.LastName))
            .ForPath(d => d.UserInfo.Image.Base64, s => s.MapFrom(m => m.Image))
            .ForPath(d => d.UserInfo.Image.Width, s => s.MapFrom(m => m.Width))
            .ForPath(d => d.UserInfo.Image.Height, s => s.MapFrom(m => m.Height))
            .ReverseMap();

        CreateMap<UserContract, UserEntity>()
            .ForMember(d => d.Id, s => s.MapFrom(m => m.Id))
            .ForMember(d => d.PasswordHash, s => s.Ignore())
            .ForMember(d => d.Role, s => s.MapFrom(m => m.Role))
            .ForPath(d => d.UserInfo.Nickname, s => s.MapFrom(m => m.Nickname))
            .ForPath(d => d.UserInfo.FirstName, s => s.MapFrom(m => m.FirstName))
            .ForPath(d => d.UserInfo.LastName, s => s.MapFrom(m => m.LastName))
            .ForPath(d => d.UserInfo.Image.Base64, s => s.MapFrom(m => m.Image))
            .ForMember(d => d.Created, s => s.MapFrom(m => m.Created))
            .ForPath(d => d.UserInfo.Updated, s => s.MapFrom(m => m.Updated))
            .ReverseMap();

        CreateMap<CommentContract, CommentEntity>()
            .ForMember(d => d.Id, s => s.MapFrom(m => m.Id))
            .ForMember(d => d.Created, s => s.MapFrom(m => m.Created))
            .ForMember(d => d.Updated, s => s.MapFrom(m => m.Updated))
            .ForMember(d => d.ArticleId, s => s.MapFrom(m => m.ArticleId))
            .ForPath(d => d.UserInfo.Nickname, s => s.MapFrom(m => m.Author.Nickname))
            .ForPath(d => d.UserInfo.FirstName, s => s.MapFrom(m => m.Author.FirstName))
            .ForPath(d => d.UserInfo.LastName, s => s.MapFrom(m => m.Author.LastName))
            .ForPath(d => d.UserInfo.Image.Base64, s => s.MapFrom(m => m.Author.Image))
            .ForMember(d => d.Text, s => s.MapFrom(m => m.Text))
            .ReverseMap();

        CreateMap<ArticleContract, ArticleEntity>()
            .ForMember(d => d.Id, s => s.MapFrom(m => m.Id))
            .ForMember(d => d.Created, s => s.MapFrom(m => m.Created))
            .ForMember(d => d.Updated, s => s.MapFrom(m => m.Updated))
            .ForPath(d => d.UserInfo.Nickname, s => s.MapFrom(m => m.Author.Nickname))
            .ForPath(d => d.UserInfo.FirstName, s => s.MapFrom(m => m.Author.FirstName))
            .ForPath(d => d.UserInfo.LastName, s => s.MapFrom(m => m.Author.LastName))
            .ForPath(d => d.UserInfo.Image.Base64, s => s.MapFrom(m => m.Author.Image))
            .ForMember(d => d.Description, s => s.MapFrom(m => m.Description))
            .ForMember(d => d.Header, s => s.MapFrom(m => m.Header))
            .ForMember(d => d.Comments, s => s.MapFrom(m => m.Comments))
            .ForPath(d => d.LeadingImage.Base64, s => s.MapFrom(m => m.LeadingImage))
            .ReverseMap();
    }
}
