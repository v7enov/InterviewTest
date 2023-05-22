using CommonLib.Events;
using CommonLib.Models;
using CommonLib.Models.Abstractions;
using CommonLib.Watchers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

[TestClass]
public class NetworkAdaptersWatcherTests
{
    [TestMethod]
    public async Task NetworkAdaptersWatcherShouldEmitEvent()
    {
        var eventArrived = await Setup(AdaptersGetDownAfter100Ms);
        Assert.IsTrue(eventArrived);
    }

    [TestMethod]
    public async Task NetworkAdaptersWatcherShouldntEmitEvent()
    {
        var eventArrived = await Setup(AdaptersStayAlive);
        Assert.IsTrue(!eventArrived);
    }


    private async Task<bool> Setup(Func<IAsyncEnumerable<IEnumerable<NetworkAdapter>>> adaptersSequence)
    {
        var eventArrived = false;
        var cts = new CancellationTokenSource();

        var notifier = new SystemEventNotifier();
        notifier.EventArrived += (sender, args) =>
        {
            if (args.Type == EventType.EthernetAdapterDown)
                eventArrived = true;
        };

        var networkAdapterFactoryMock = new Mock<INetworkAdapterFactory>();
        networkAdapterFactoryMock.Setup(x => x.GetNetworkAdapters(It.IsAny<CancellationToken>())).Returns(adaptersSequence);
        var ethernetAdaptersWatcher = new EthernetAdaptersWatcher(new LoggerFactory().CreateLogger<EthernetAdaptersWatcher>(),
            notifier,
            networkAdapterFactoryMock.Object);

        await ethernetAdaptersWatcher.StartAsync(cts.Token);
        await Task.Delay(100);

        return eventArrived;
    }


    private async IAsyncEnumerable<IEnumerable<NetworkAdapter>> AdaptersStayAlive()
    {
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
        };
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
        };
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
        };
    }

    private async IAsyncEnumerable<IEnumerable<NetworkAdapter>> AdaptersGetDownAfter100Ms()
    {
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
        };
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Up)
        };
        yield return new NetworkAdapter[]
        {
            new NetworkAdapter(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet, System.Net.NetworkInformation.OperationalStatus.Down)
        };
    }
}
