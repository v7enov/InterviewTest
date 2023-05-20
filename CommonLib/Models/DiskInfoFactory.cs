using CommonLib.Models.Abstractions;

namespace CommonLib.Models;

public class DiskInfoFactory : IDiskInfoFactory
{
    private readonly string _mountPoint;

    public DiskInfoFactory(string mountPoint)
    {
        _mountPoint = mountPoint;
    }

    public IDiskInfo Create()
    {
        return new DiskInfo(_mountPoint);
    }
}
