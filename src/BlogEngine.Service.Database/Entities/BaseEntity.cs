using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Базовая сущность
/// </summary>
internal class BaseEntity
{
    /// <summary>
    ///     Дата создания записи в БД
    /// </summary>
    [Column("created")]
    public DateTime Created { get; set; }

    /// <summary>
    ///     Время обновления сущности в БД
    /// </summary>
    [Column("updated")]
    public DateTime Updated { get; set; }

    /// <inheritdoc />
    public virtual void EntityAddedHandle() 
        => Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

    /// <inheritdoc />
    public virtual void EntityUpdatedHandle() 
        => Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

}
