using CommonLib.Abstractions;

namespace CommonLib.Events;

public class SystemEventNotifier : ISystemEventNotifier
{
    public event EventHandler<ISystemEvent>? EventArrived;

    public void OnNewSystemEvent(ISystemEvent systemEvent)
    {
        EventArrived?.Invoke(this, systemEvent);
    }
}
