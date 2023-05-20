namespace CommonLib.Abstractions;

public interface ISystemEventNotifier
{
    event EventHandler<ISystemEvent> EventArrived;
    void OnNewSystemEvent(ISystemEvent systemEvent);
}
