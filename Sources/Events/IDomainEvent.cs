using MediatR;

namespace MyDotNetEventSourcedProject.Sources.Events;

public interface IDomainEvent: INotification
{
    string EventType { get; }
    // DateTime CreatedAt { get; }
}

public abstract record EventBase(string CorrelationId) : IDomainEvent
{
    public string EventType { get { return GetType().FullName; } }
    
    // public DateTime CreatedAt { get; } = DateTime.UtcNow;
    // public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
    // public abstract void Flatten();
}
