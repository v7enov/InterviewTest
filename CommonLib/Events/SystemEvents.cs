using CommonLib.Abstractions;

namespace CommonLib.Events;

public enum EventType
{
    NoFreeSpace,
    EthernetAdapterDown
}

public class NoFreeSpaceEvent : ISystemEvent
{
    public EventType Type => EventType.NoFreeSpace;

    public string EventDescription { get; init; }

    public NoFreeSpaceEvent(string description)
    {
        EventDescription = description;
    }
}

public class EthernetAdapterDownEvent : ISystemEvent
{
    public EventType Type => EventType.EthernetAdapterDown;

    public string EventDescription { get; init; }

    public EthernetAdapterDownEvent(string description)
    {
        EventDescription = description;
    }
}
