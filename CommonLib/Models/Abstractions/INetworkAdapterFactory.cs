namespace CommonLib.Models.Abstractions;

public interface INetworkAdapterFactory
{
    IEnumerable<INetworkAdapter> Create();
}