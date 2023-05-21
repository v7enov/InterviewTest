using CommonLib.Abstractions;

namespace CommonLib.Events;

public enum EventType
{
    NoFreeSpace,
    EthernetAdapterDown
}

public class SystemEvent
{
    public EventType Type { get; init; }

    public string EventDescription { get; init; }

    public SystemEvent(EventType Type, string description)
    {
        this.Type = Type;
        EventDescription = description;
    }
}
