using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Abstractions;
using MediatR;

namespace Domain.Common;

public class Entity<TId> where TId : struct
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity()
    {
    }

    public TId Id { get; protected set; }

    public int CreatedBy { get; }

    public DateTime CreatedOn { get; }

    public int? ModifiedBy { get; }

    public DateTime? ModifiedOn { get; }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public List<IDomainEvent> GetDomainEvents()
    {
        return [.. _domainEvents];
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
