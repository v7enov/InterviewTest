namespace CommonLib.Abstractions;

//split into 2 interfaces?
public interface ISystemEventNotifier
{
    event EventHandler<ISystemEvent> EventArrived;
    void OnNewSystemEvent(ISystemEvent systemEvent);
}
