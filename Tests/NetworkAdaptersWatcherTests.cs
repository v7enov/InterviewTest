using CommonLib.Events;
using CommonLib.Models;
using CommonLib.Models.Abstractions;
using CommonLib.Watchers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

[TestClass]
public class NetworkAdaptersWatcherTests
{
    [TestMethod]
    public async Task NetworkAdaptersWatcherShouldEmitEvent()
    {
        var networkAdapterEventArrived = false;
        var cts = new CancellationTokenSource();

        var notifier = new SystemEventNotifier();
        notifier.EventArrived += (sender, args) =>
        {
            if (args.Type == EventType.EthernetAdapterDown)
                networkAdapterEventArrived = true;
        };

        var networkAdapterFactoryMock = new Mock<INetworkAdapterFactory>();
        networkAdapterFactoryMock.SetupSequence(
            x => x.GetNetworkAdapters())
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
            })
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
            })
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Down)
            });
        var ethernetAdaptersWatcher = new EthernetAdaptersWatcher(new LoggerFactory().CreateLogger<EthernetAdaptersWatcher>(),
            notifier, 
            networkAdapterFactoryMock.Object);

        await ethernetAdaptersWatcher.StartAsync(cts.Token);
        await Task.Delay(TimeSpan.FromSeconds(5));

        cts.Cancel();

        Assert.IsTrue(networkAdapterEventArrived);
    }

    [TestMethod]
    public async Task NetworkAdaptersWatcherShouldntEmitEvent()
    {
        var networkAdapterEventArrived = false;
        var cts = new CancellationTokenSource();

        var notifier = new SystemEventNotifier();
        notifier.EventArrived += (sender, args) =>
        {
            if (args.Type == EventType.EthernetAdapterDown)
                networkAdapterEventArrived = true;
        };

        var networkAdapterFactoryMock = new Mock<INetworkAdapterFactory>();
        networkAdapterFactoryMock.SetupSequence(
            x => x.GetNetworkAdapters())
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
            })
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
            })
            .Returns(new List<NetworkAdapter>
            {
                new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
            });
        var ethernetAdaptersWatcher = new EthernetAdaptersWatcher(new LoggerFactory().CreateLogger<EthernetAdaptersWatcher>(),
            notifier,
            networkAdapterFactoryMock.Object);

        await ethernetAdaptersWatcher.StartAsync(cts.Token);
        await Task.Delay(TimeSpan.FromSeconds(5));

        cts.Cancel();

        Assert.IsTrue(!networkAdapterEventArrived);
    }
}
