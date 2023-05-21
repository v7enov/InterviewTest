namespace CommonLib.Models.Abstractions;

//could be a class as well
public interface IDiskInfo
{
    /// <summary>
    /// Available free space on drive in bytes
    /// </summary>
    long AvailableFreeSpace { get; }
}