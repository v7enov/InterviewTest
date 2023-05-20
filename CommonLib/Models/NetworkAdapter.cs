using CommonLib.Models.Abstractions;
using System.Net.NetworkInformation;

namespace CommonLib.Models;

public class NetworkAdapter : INetworkAdapter
{
    private readonly NetworkInterface _networkInterface;

    public NetworkAdapter(NetworkInterface networkInterface)
    {
        _networkInterface = networkInterface;
    }

    public NetworkInterfaceType InterfaceType => _networkInterface.NetworkInterfaceType;

    public OperationalStatus OperationalStatus => _networkInterface.OperationalStatus;
}
