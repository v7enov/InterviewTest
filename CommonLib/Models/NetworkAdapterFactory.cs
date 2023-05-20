using CommonLib.Models.Abstractions;
using System.Net.NetworkInformation;

namespace CommonLib.Models;

public class NetworkAdapterFactory : INetworkAdapterFactory
{
    public IEnumerable<INetworkAdapter> Create()
    {
        return NetworkInterface.GetAllNetworkInterfaces().Select(i => new NetworkAdapter(i));
    }
}
