using CommonLib.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Models;

public class DiskInfo : IDiskInfo
{
    private readonly DriveInfo _driveInfo;
    public long AvailableFreeSpace => _driveInfo.AvailableFreeSpace;

    public DiskInfo(string mountPoint)
    {
        _driveInfo = new DriveInfo(mountPoint);
    }
}
