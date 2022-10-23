using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица статей
/// </summary>
[Table("articles")]
public sealed class ArticleEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Заголовок статьи
    /// </summary>
    [Column("header")]
    public string Header { get; set; } = default!;

    /// <summary>
    ///     Описание статьи
    /// </summary>
    [Column("desc")]
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Идентификатор ведущего изображения
    /// </summary>
    [Column("leading_image_id")]
    [ForeignKey(nameof(LeadingImage))]
    public long? LeadingImageId { get; set; }

    /// <summary>
    ///     Ведущее изображение
    /// </summary>
    public ImageEntity? LeadingImage { get; set; }

    /// <summary>
    ///     Идентификатор ведущего изображения
    /// </summary>
    [Column("issue_id")]
    [ForeignKey(nameof(Issue))]
    public long IssueId { get; set; }

    /// <summary>
    ///     Ведущее изображение
    /// </summary>
    public IssueEntity Issue { get; set; } = default!;

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
    ///     Текст статьи, размеченный HTML
    /// </summary>
    [Column("text")]
    public string Text { get; set; } = default!;

    /// <summary>
    ///     Тэги
    /// </summary>
    [Column("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    /// <summary>
    ///     Комментарии к статье
    /// </summary>
    public IList<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    public static void Setup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleEntity>()
            .HasMany(_ => _.Comments)
            .WithOne(_ => _.Article)
            .HasForeignKey(_ => _.ArticleId)
            .HasConstraintName("FK_articles_comments");
    }
}
