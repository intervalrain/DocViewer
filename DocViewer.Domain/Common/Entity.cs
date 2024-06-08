namespace DocViewer.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected readonly List<IDomainEvent> _domainEvents;

    public Entity(Guid id)
    {
        _domainEvents = new List<IDomainEvent>();
        Id = id;
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();
        return copy;
    }
}

