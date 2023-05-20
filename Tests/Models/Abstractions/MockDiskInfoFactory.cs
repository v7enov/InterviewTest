using CommonLib.Models;
using CommonLib.Models.Abstractions;
using System.Reflection.Metadata;

namespace Tests.Models.Abstractions;

public class MockDiskInfoFactory : IDiskInfoFactory
{
    private static DiskInfoMock _diskInfoMock;

    public MockDiskInfoFactory()
    {
        _diskInfoMock = new DiskInfoMock(TimeSpan.FromSeconds(5));
    }
    public IDiskInfo Create()
    {
        return _diskInfoMock;
    }
}
