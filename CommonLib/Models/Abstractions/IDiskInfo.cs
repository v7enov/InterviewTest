namespace CommonLib.Models.Abstractions;

public interface IDiskInfo
{
    /// <summary>
    /// Available free space on drive in bytes
    /// </summary>
    long AvailableFreeSpace { get; }
}