using CommonLib.Models.Abstractions;

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
}
