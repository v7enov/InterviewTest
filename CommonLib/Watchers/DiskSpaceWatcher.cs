using CommonLib.Abstractions;
using CommonLib.Events;
using CommonLib.Models.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommonLib.Watchers;

public class DiskSpaceWatcher : BackgroundService
{
    private readonly ILogger<DiskSpaceWatcher> _logger;
    private readonly ISystemEventNotifier _systemEventNotifier;
    private readonly IDiskInfoFactory _diskInfoFactory;

    public DiskSpaceWatcher(
        ILogger<DiskSpaceWatcher> logger, 
        ISystemEventNotifier systemEventNotifier,
        IDiskInfoFactory diskInfoFactory
        )
    {
        _logger = logger;
        _systemEventNotifier = systemEventNotifier;
        _diskInfoFactory = diskInfoFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var diskInfo = _diskInfoFactory.Create();

                var freeSpace = diskInfo.AvailableFreeSpace;
                var toGiga = Math.Pow(1024, 3);
                var freeSpaceInGiGa = freeSpace / toGiga;

                if (freeSpace / toGiga <= 5)
                    _systemEventNotifier.OnNewSystemEvent(new NoFreeSpaceEvent("Нет места на диске"));

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("DiskSpaceWatcher.Cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError($"DiskSpaceWatcher: {ex.Message}");
            }
        }
    }
}

