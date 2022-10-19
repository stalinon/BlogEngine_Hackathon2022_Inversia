using BlogEngine.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица пользователей
/// </summary>
[Table("users")]
internal sealed class UserEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Хэш пароля пользователя
    /// </summary>
    [Column("pwd_hash")]
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    ///     Роль пользователя
    /// </summary>
    [Column("role")]
    public UserRole Role { get; set; } = UserRole.USER;

    /// <summary>
    ///     Идентификатор информации о пользователе
    /// </summary>
    [Column("user_info_id")]
    [ForeignKey(nameof(UserInfo))]
    public long UserInfoId { get; set; }

    /// <summary>
    ///     Информация о пользователе
    /// </summary>
    public UserInfoEntity UserInfo { get; set; } = default!;

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    public static void Setup(ModelBuilder modelBuilder) 
        => modelBuilder.Entity<UserEntity>().HasIndex(x => x.UserInfoId).HasDatabaseName("IX_users_user_info_id").IsUnique();
}
