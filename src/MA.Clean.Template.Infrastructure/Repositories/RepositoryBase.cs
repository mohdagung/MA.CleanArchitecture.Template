using Microsoft.EntityFrameworkCore;
using MA.Clean.Template.Application.Common.Abstractions;

namespace MA.Clean.Template.Infrastructure.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _db;
    protected readonly DbSet<TEntity> _set;

    public RepositoryBase(DbContext db)
    {
        _db = db;
        _set = _db.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        => await _set.FindAsync(new object?[] { id }, ct);

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct = default)
        => await _set.AsNoTracking().ToListAsync(ct);

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await _set.AddAsync(entity, ct);

    public virtual Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        _set.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveAsync(TEntity entity, CancellationToken ct = default)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}