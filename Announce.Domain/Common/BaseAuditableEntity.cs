namespace Announce.Domain.Common;

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

