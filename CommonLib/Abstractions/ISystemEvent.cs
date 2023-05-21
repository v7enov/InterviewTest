using CommonLib.Events;

namespace CommonLib.Abstractions;

//could be just a class
public interface ISystemEvent
{
    EventType Type { get; }
    string EventDescription { get; }
}
