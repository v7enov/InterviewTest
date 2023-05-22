using CommonLib.Events;
using CommonLib.Models;
using CommonLib.Models.Abstractions;
using CommonLib.Watchers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

[TestClass]
public class DiskWatcherTests
{
    [TestMethod]
    public async Task DiskSpaceWatcherShouldEmitNoFreeSpaceEvent()
    {
        var diskEventArrived = false;
        var notifier = new SystemEventNotifier();

        notifier.EventArrived += (sender, args) =>
        {
            switch (args.Type)
            {
                case EventType.NoFreeSpace:
                    diskEventArrived = true;
                    break;
            }
        };

        var diskInfoFactory = new Mock<IDiskInfoFactory>();
        diskInfoFactory.Setup(x => x.GetDiskInfoAsync(It.IsAny<CancellationToken>())).Returns(new List<DiskInfo> { new DiskInfo { AvailableFreeSpace = 1_000_000_000L }, new DiskInfo { AvailableFreeSpace = 10 } }.ToAsyncEnumerable());
        var diskSpaceWatcher = new DiskSpaceWatcher(new LoggerFactory().CreateLogger<DiskSpaceWatcher>(), notifier, diskInfoFactory.Object);
        var cancellationTokenSource = new CancellationTokenSource();
        await diskSpaceWatcher.StartAsync(cancellationTokenSource.Token);
        Assert.IsTrue(diskEventArrived);
    }

    [TestMethod]
    public async Task DiskSpaceWatcherShouldntEmitNoFreeSpaceEventTooEarly()
    {
        var diskEventArrived = false;
        var notifier = new SystemEventNotifier();

        notifier.EventArrived += (sender, args) =>
        {
            switch (args.Type)
            {
                case EventType.NoFreeSpace:
                    diskEventArrived = true;
                    break;
            }
        };

        async IAsyncEnumerable<DiskInfo> Mock()
        {
            yield return new DiskInfo { AvailableFreeSpace = 7368709120 };
            yield return new DiskInfo { AvailableFreeSpace = 7368709120 };
            yield return new DiskInfo { AvailableFreeSpace = 7368709120 };
            yield return new DiskInfo { AvailableFreeSpace = 7368709120 };
            await Task.Delay(TimeSpan.FromSeconds(10));
            yield return new DiskInfo { AvailableFreeSpace = 10 };
        }
        var diskInfoFactory = new Mock<IDiskInfoFactory>();
        diskInfoFactory.Setup(x => x.GetDiskInfoAsync(It.IsAny<CancellationToken>())).Returns(Mock());
        var diskSpaceWatcher = new DiskSpaceWatcher(new LoggerFactory().CreateLogger<DiskSpaceWatcher>(), notifier, diskInfoFactory.Object);
        var cancellationTokenSource = new CancellationTokenSource();
        await diskSpaceWatcher.StartAsync(cancellationTokenSource.Token);
        await Task.Delay(100);
        cancellationTokenSource.Cancel();
        Assert.IsFalse(diskEventArrived);
    }
}