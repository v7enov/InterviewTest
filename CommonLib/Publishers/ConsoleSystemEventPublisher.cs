using CommonLib.Abstractions;

namespace CommonLib.Events;

public class ConsoleSystemEventPublisher : ISystemEventPublisher
{
    private readonly ISystemEventNotifier _systemEventNotifier;

    public ConsoleSystemEventPublisher(ISystemEventNotifier systemEventNotifier)
    {
        _systemEventNotifier = systemEventNotifier;
        _systemEventNotifier.EventArrived += EventArrived;
    }

    private void EventArrived(object? sender, ISystemEvent e)
    {
        Console.WriteLine($"New event arrived - Type: {e.Type}; Desc: {e.EventDescription}");
    }

    public void Dispose()
    {
        _systemEventNotifier.EventArrived -= EventArrived;  
    }
}
