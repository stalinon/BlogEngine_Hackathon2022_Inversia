using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица комментариев
/// </summary>
[Table("comments")]
public sealed class CommentEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Текст комментария
    /// </summary>
    [Column("text")]
    public string Text { get; set; } = default!;

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
    ///     Идентификатор статьи
    /// </summary>
    [Column("article_id")]
    [ForeignKey(nameof(ArticleEntity))]
    public long ArticleId { get; set; }

    /// <summary>
    ///     Информация о статье
    /// </summary>
    public ArticleEntity Article { get; set; } = default!;

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    public static void Setup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentEntity>().HasIndex(e => e.UserInfoId).HasDatabaseName("IX_comments_user_info_id");
        modelBuilder.Entity<CommentEntity>().HasIndex(e => e.ArticleId).HasDatabaseName("IX_comments_article_id");
    }
}
