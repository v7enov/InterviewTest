using CommonLib.Models.Abstractions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace CommonLib.Models;

public class NetworkAdapterFactory : INetworkAdapterFactory
{
    public async IAsyncEnumerable<IEnumerable<NetworkAdapter>> GetNetworkAdapters([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            var adapters = Array.Empty<NetworkAdapter>();

            try
            {
                adapters = NetworkInterface.GetAllNetworkInterfaces().Select(i => new NetworkAdapter(i.NetworkInterfaceType, i.OperationalStatus)).ToArray();
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
            catch (OperationCanceledException)
            { 
                break;
            }

            yield return adapters;
        }
    }
}
