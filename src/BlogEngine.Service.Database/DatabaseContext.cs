using BCrypt.Net;
using BlogEngine.Core;
using BlogEngine.Service.Database.Entities;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogEngine.Service.Database;

/// <summary>
///     Контекст базы данных
/// </summary>
internal class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    /// <summary>
    ///     Статьи
    /// </summary>
    public DbSet<ArticleEntity> Articles { get; } = default!;

    /// <summary>
    ///     Комментарии
    /// </summary>
    public DbSet<CommentEntity> Comments { get; } = default!;

    /// <summary>
    ///     Изображения
    /// </summary>
    public DbSet<ImageEntity> Images { get; } = default!;

    /// <summary>
    ///     Пользователи
    /// </summary>
    public DbSet<UserEntity> Users { get; } = default!;

    /// <summary>
    ///     Информация о пользователях
    /// </summary>
    public DbSet<UserInfoEntity> UserInfos { get; } = default!;

    /// <summary>
    ///     Выпуски
    /// </summary>
    public DbSet<IssueEntity> Issues { get; } = default!;

    #region Override Methods

    /// <inheritdoc/>
    public override int SaveChanges()
    {
        SetPropertiesValueTime();
        return base.SaveChanges();
    }

    /// <inheritdoc/>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetPropertiesValueTime();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetPropertiesValueTime();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetPropertiesValueTime();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseExceptionProcessor();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArticleEntity.Setup(modelBuilder);
        CommentEntity.Setup(modelBuilder);
        ImageEntity.Setup(modelBuilder);
        UserInfoEntity.Setup(modelBuilder);
        UserEntity.Setup(modelBuilder);
        IssueEntity.Setup(modelBuilder);
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Устанавливает время создания/обновления сущностей
    /// </summary>
    private static void SetEntityTimes(IEnumerable<EntityEntry> entries)
    {
        entries = entries
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entity.Created = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                    entity.EntityAddedHandle();
                    break;

                case EntityState.Modified:
                    entity.EntityUpdatedHandle();
                    break;
            }
        }
    }

    /// <summary>
    ///     Установить значение свойствам времени
    /// </summary>
    private void SetPropertiesValueTime()
    {
        var entries = ChangeTracker.Entries();
        SetEntityTimes(entries);
    }

    #endregion

}
