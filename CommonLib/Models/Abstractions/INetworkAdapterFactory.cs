using System.Runtime.CompilerServices;

namespace CommonLib.Models.Abstractions;

public interface INetworkAdapterFactory
{
    IAsyncEnumerable<IEnumerable<NetworkAdapter>> GetNetworkAdapters(CancellationToken cancellationToken);
}