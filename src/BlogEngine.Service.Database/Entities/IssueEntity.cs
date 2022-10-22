using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица выпусков журнала
/// </summary>
[Table("issues")]
public sealed class IssueEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Номер выпуска
    /// </summary>
    [Column("issue_number")]
    public long IssueNumber { get; set; }

    /// <summary>
    ///     Дата выпуска
    /// </summary>
    [Column("date")]
    public DateOnly Date { get; set; }

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
    ///     Статьи
    /// </summary>
    public IList<ArticleEntity> Articles { get; set; } = new List<ArticleEntity>();

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    public static void Setup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueEntity>().HasIndex(x => x.IssueNumber).HasDatabaseName("IX_issues_issue_number").IsUnique();

        modelBuilder.Entity<IssueEntity>()
            .HasMany(_ => _.Articles)
            .WithOne(_ => _.Issue)
            .HasForeignKey(_ => _.IssueId)
            .HasConstraintName("FK_issues_article");
    }
}
