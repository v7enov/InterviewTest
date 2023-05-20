using System.Net.NetworkInformation;

namespace CommonLib.Models.Abstractions;

public interface INetworkAdapter
{
    NetworkInterfaceType InterfaceType { get; }
    OperationalStatus OperationalStatus { get; }
}
