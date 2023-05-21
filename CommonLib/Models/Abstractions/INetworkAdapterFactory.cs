namespace CommonLib.Models.Abstractions;

public interface INetworkAdapterFactory
{
    IEnumerable<NetworkAdapter> GetNetworkAdapters();
}