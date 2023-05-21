using CommonLib.Models.Abstractions;
using System.Net.NetworkInformation;

namespace CommonLib.Models;

public class NetworkAdapterFactory : INetworkAdapterFactory
{
    public IEnumerable<NetworkAdapter> GetNetworkAdapters()
    {
        return NetworkInterface.GetAllNetworkInterfaces().Select(i => new NetworkAdapter(i.NetworkInterfaceType, i.OperationalStatus));
    }
}
