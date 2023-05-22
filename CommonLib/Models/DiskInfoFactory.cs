using CommonLib.Models.Abstractions;
using System.Runtime.CompilerServices;

namespace CommonLib.Models;

public class DiskInfoFactory : IDiskInfoFactory
{
    private readonly DriveInfo _driveInfo;

    public DiskInfoFactory(string mountPoint)
    {
        _driveInfo = new DriveInfo(mountPoint);
    }

    public DiskInfo GetDiskInfo()
    {
        return new DiskInfo()
        {
            AvailableFreeSpace = _driveInfo.AvailableFreeSpace,
        };
    }

    public async IAsyncEnumerable<DiskInfo> GetDiskInfoAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return new DiskInfo
            {
                AvailableFreeSpace = _driveInfo.AvailableFreeSpace
            };
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
