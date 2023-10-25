namespace MyDotNetEventSourcedProject.Sources;

public interface IEventStore
{
    IEnumerable<IDomainEvent> Events { get; }

    void PushNewEvent(IDomainEvent @event);
}
