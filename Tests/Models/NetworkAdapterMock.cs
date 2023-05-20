using CommonLib.Models.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

namespace CommonLib.Models;

public class NetworkAdapterMock : INetworkAdapter
{
    private readonly ILogger _logger;

    public NetworkInterfaceType InterfaceType { get; private set; }
    public OperationalStatus OperationalStatus { get; private set; }

    public NetworkAdapterMock(ILogger logger, TimeSpan downAfter)
    {
        InterfaceType = NetworkInterfaceType.Ethernet;
        OperationalStatus = OperationalStatus.Up;
        _logger = logger;


        Task.Run(async () =>
        {
            await Task.Delay(downAfter);
            OperationalStatus = OperationalStatus.Down;
        });
    }
}
