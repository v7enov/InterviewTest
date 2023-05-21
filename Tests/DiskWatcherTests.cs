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
        diskInfoFactory.SetupSequence(x => x.GetDiskInfo()).Returns(new DiskInfo { AvailableFreeSpace = 1_000_000_000L }).Returns(new DiskInfo { AvailableFreeSpace = 10 });

        var diskSpaceWatcher = new DiskSpaceWatcher(new LoggerFactory().CreateLogger<DiskSpaceWatcher>(), notifier, diskInfoFactory.Object);
        var cancellationTokenSource = new CancellationTokenSource();
        await diskSpaceWatcher.StartAsync(cancellationTokenSource.Token);
        await Task.Delay(3000);
        cancellationTokenSource.Cancel();
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

        var diskInfoFactory = new Mock<IDiskInfoFactory>();
        diskInfoFactory.SetupSequence(x => x.GetDiskInfo())
            .Returns(new DiskInfo { AvailableFreeSpace = 7368709120 })
            .Returns(new DiskInfo { AvailableFreeSpace = 7368709120 })
            .Returns(new DiskInfo { AvailableFreeSpace = 7368709120 })
            .Returns(new DiskInfo { AvailableFreeSpace = 7368709120 })
            .Returns(new DiskInfo { AvailableFreeSpace = 10 });

        var diskSpaceWatcher = new DiskSpaceWatcher(new LoggerFactory().CreateLogger<DiskSpaceWatcher>(), notifier, diskInfoFactory.Object);
        var cancellationTokenSource = new CancellationTokenSource();
        await diskSpaceWatcher.StartAsync(cancellationTokenSource.Token);
        await Task.Delay(3000);
        cancellationTokenSource.Cancel();
        Assert.IsFalse(diskEventArrived);
    }
}