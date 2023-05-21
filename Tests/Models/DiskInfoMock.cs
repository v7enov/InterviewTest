using CommonLib.Models.Abstractions;

namespace CommonLib.Models;

public class DiskInfoMock : IDiskInfo
{
    public long AvailableFreeSpace { get; init; }
}
