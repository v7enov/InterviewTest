using CommonLib.Abstractions;
using CommonLib.Events;
using CommonLib.Models;
using CommonLib.Models.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

namespace CommonLib.Watchers;

public class EthernetAdaptersWatcher : BackgroundService
{
    private readonly ILogger<EthernetAdaptersWatcher> _logger;
    private readonly ISystemEventNotifier _systemEventNotifier;
    private readonly INetworkAdapterFactory _networkAdapterFactory;

    public EthernetAdaptersWatcher(
        ILogger<EthernetAdaptersWatcher> logger, 
        ISystemEventNotifier systemEventNotifier,
        INetworkAdapterFactory networkAdapterFactory)
    {
        _logger = logger;
        _systemEventNotifier = systemEventNotifier;
        _networkAdapterFactory = networkAdapterFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var adapters = _networkAdapterFactory.GetNetworkAdapters();
                
                if (adapters.Any(a => a.InterfaceType == NetworkInterfaceType.Ethernet && a.OperationalStatus != OperationalStatus.Up))
                    _systemEventNotifier.OnNewSystemEvent(new SystemEvent(EventType.EthernetAdapterDown, "Some ethernet adapter is currently down"));


                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            catch (OperationCanceledException){}
        }
    }
}
