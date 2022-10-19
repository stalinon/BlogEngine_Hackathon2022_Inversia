namespace BlogEngine.Service.Database.Entities;

/// <summary>
///     Признак наличия идентификатора
/// </summary>
public interface IHasEntityId
{
    /// <summary>
    ///     Идентификатор в БД
    /// </summary>
    public long Id { get; set; }
}
