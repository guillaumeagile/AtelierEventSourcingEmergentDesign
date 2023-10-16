namespace MyDotNetEventSourcedProject;

public class FakeEventStore: IEventStore
{
    private IList<IDomainEvent> _events;

    public FakeEventStore()
    {
        _events = new List<IDomainEvent>();
    }

    public IEnumerable<IDomainEvent> Events
    {
        get => _events.ToList();
    }

    public void PushNewEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}
