namespace CommonLib.Models.Abstractions;

public interface IDiskInfoFactory
{
    IAsyncEnumerable<DiskInfo> GetDiskInfoAsync(CancellationToken cancellationToken);
}