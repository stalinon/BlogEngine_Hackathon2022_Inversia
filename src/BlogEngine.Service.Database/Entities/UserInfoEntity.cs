using BlogEngine.Core;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица информации о пользователе
/// </summary>
[Table("user_info")]
public sealed class UserInfoEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Никнейм пользователя
    /// </summary>
    [Column("nickname")]
    public string Nickname { get; set; } = default!;

    /// <summary>
    ///     Имя
    /// </summary>
    [Column("first_name")]
    public string FirstName { get; set; } = default!;

    /// <summary>
    ///     Фамилия
    /// </summary>
    [Column("last_name")]
    public string LastName { get; set; } = default!;

    /// <summary>
    ///     Идентификатор изображения профиля
    /// </summary>
    [Column("image_id")]
    [ForeignKey(nameof(Image))]
    public long? ImageId { get; set; }

    /// <summary>
    ///     Изображение профиля
    /// </summary>
    public ImageEntity? Image { get; set; } = default!;

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    public static void Setup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfoEntity>().HasIndex(e => e.Nickname).HasDatabaseName("IX_user_info_nickname").IsUnique();
        modelBuilder.Entity<UserInfoEntity>().HasIndex(e => e.FirstName).HasDatabaseName("IX_user_info_first_name");
        modelBuilder.Entity<UserInfoEntity>().HasIndex(e => e.LastName).HasDatabaseName("IX_user_info_last_name");
    }
}
