using CommonLib.Abstractions;
using CommonLib.Events;
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
        _systemEventNotifier.OnNewSystemEvent(new EthernetAdapterDownEvent("ok"));
        await Task.CompletedTask;
    }
}
