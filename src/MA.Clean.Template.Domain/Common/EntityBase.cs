namespace MA.Clean.Template.Domain.Common;

public abstract class EntityBase<TKey>
{
    public TKey Id { get; protected set; } = default!;
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
    public int? CreatedBy { get; protected set; }
    public DateTime? ModifiedAtUtc { get; protected set; }
    public int? ModifiedBy { get; protected set; }

    protected void MarkCreated(int? userId = null)
    {
        CreatedAtUtc = DateTime.UtcNow;
        CreatedBy = userId;
    }

    protected void MarkModified(int? userId = null)
    {
        ModifiedAtUtc = DateTime.UtcNow;
        ModifiedBy = userId;
    }
}

public interface IAggregateRoot { }