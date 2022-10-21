using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Таблица изображений
/// </summary>
[Table("image_strings")]
public sealed class ImageEntity : BaseEntity, IHasEntityId
{
    /// <inheritdoc />
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Изображение в формате Base64
    /// </summary>
    [Column("base64")]
    public string Base64 { get; set; } = string.Empty;

    /// <summary>
    ///     Оригинальная ширина изображения
    /// </summary>
    [Column("width")]
    public int Width { get; set; }

    /// <summary>
    ///     Оригинальная высота изображения
    /// </summary>
    [Column("height")]
    public int Height { get; set; }

    /// <summary>
    ///     Инициализация свойств таблицы
    /// </summary>
    [SuppressMessage("Style", "IDE0060")]
    public static void Setup(ModelBuilder modelBuilder)
    { }
}
