using CommonLib.Abstractions;

namespace CommonLib.Events;

public class SystemEventNotifier : ISystemEventNotifier
{
    public event EventHandler<SystemEvent>? EventArrived;

    public void OnNewSystemEvent(SystemEvent systemEvent)
    {
        EventArrived?.Invoke(this, systemEvent);
    }
}
