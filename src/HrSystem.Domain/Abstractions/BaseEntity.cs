namespace HrSystem.Domain.Abstractions;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? UpdatedDate { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }

    protected void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void SetUpdated()
    {
        UpdatedDate = DateTime.UtcNow;
    }
}
