using CommonLib.Events;

namespace CommonLib.Abstractions;

//split into 2 interfaces?
public interface ISystemEventNotifier
{
    event EventHandler<SystemEvent> EventArrived;
    void OnNewSystemEvent(SystemEvent systemEvent);
}
