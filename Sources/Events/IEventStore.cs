namespace MyDotNetEventSourcedProject.Sources.Events;

public interface IEventStore
{
    IEnumerable<IDomainEvent> Events { get; }

    void PushNewEvent(IDomainEvent @event);
}
