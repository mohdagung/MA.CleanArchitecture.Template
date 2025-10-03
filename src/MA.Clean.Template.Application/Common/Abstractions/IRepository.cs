namespace MA.Clean.Template.Application.Common.Abstractions;

public interface IReadRepository<TEntity, TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default);
}

public interface IRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>
{
    Task AddAsync(TEntity entity, CancellationToken ct = default);
    Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task RemoveAsync(TEntity entity, CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}