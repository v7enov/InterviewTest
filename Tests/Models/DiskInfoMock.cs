using CommonLib.Models.Abstractions;

namespace CommonLib.Models;

public class DiskInfoMock : IDiskInfo
{
    public long AvailableFreeSpace { get; private set; }

    public DiskInfoMock(TimeSpan insufficientAfter)
    {
        AvailableFreeSpace = 45666668766;

        Task.Run(async () =>
        {
            await Task.Delay(insufficientAfter);
            AvailableFreeSpace = 4666668766;
        });
    }
}
