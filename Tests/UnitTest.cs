using CommonLib.Abstractions;
using CommonLib.Events;
using CommonLib.Models;
using CommonLib.Models.Abstractions;
using CommonLib.Watchers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tests.Models.Abstractions;

namespace Tests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public async Task TestDisksAndEthernetAdaptersEventArrivedWithin10Seconds()
    {
        var host = Setup();
        var diskEventArrived = false;
        var ethAdapterEventArrived = false;
        var run = host.StartAsync();

        var notifier = host.Services.GetService<ISystemEventNotifier>();
        notifier.EventArrived += (sender, args) =>
        {
            switch (args.Type)
            {
                case EventType.NoFreeSpace:
                    diskEventArrived = true;
                    break;
                case EventType.EthernetAdapterDown:
                    ethAdapterEventArrived = true;
                    break;
            }
        };

        await Task.Delay(TimeSpan.FromSeconds(10));
        Assert.IsTrue(diskEventArrived && ethAdapterEventArrived); 
    }

    [TestMethod]
    public async Task AreAllServicesCancelledAfter500Ms()
    {
        var host = Setup();
        var run = host.StartAsync();
        var cancel = host.StopAsync();
        var wait = Task.Delay(500);

        var completed = await Task.WhenAny(cancel, wait);

        Assert.IsTrue(completed == cancel);
    }

    private IHost Setup()
    {
        return Host.CreateDefaultBuilder(Array.Empty<string>()).ConfigureServices((context, serviceCollection) =>
        {
            serviceCollection.AddSingleton<ISystemEventNotifier, SystemEventNotifier>();
            serviceCollection.AddSingleton<INetworkAdapterFactory, MockNetworkAdapterFactory>();
            serviceCollection.AddSingleton<IDiskInfoFactory, MockDiskInfoFactory>();

            serviceCollection.AddHostedService<DiskSpaceWatcher>();
            serviceCollection.AddHostedService<EthernetAdaptersWatcher>();
        }).Build();
    }
}