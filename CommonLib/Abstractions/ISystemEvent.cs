using CommonLib.Events;

namespace CommonLib.Abstractions;

public interface ISystemEvent
{
    EventType Type { get; }
    string EventDescription { get; }
}
