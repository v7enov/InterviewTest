using System.Net.NetworkInformation;

namespace CommonLib.Models;

public class NetworkAdapter 
{
    public NetworkInterfaceType InterfaceType { get; init; }

    public OperationalStatus OperationalStatus { get; init; }

    public NetworkAdapter(NetworkInterfaceType networkInterfaceType, OperationalStatus operationalStatus)
    {
        InterfaceType = networkInterfaceType;
        OperationalStatus = operationalStatus;  
    }
}
