using CommonLib.Models;
using CommonLib.Models.Abstractions;
using Microsoft.Extensions.Logging;

namespace Tests.Models.Abstractions;

public class MockNetworkAdapterFactory : INetworkAdapterFactory
{
    private readonly ILogger<MockNetworkAdapterFactory> _logger;
    private readonly List<INetworkAdapter> _adapters;

    public MockNetworkAdapterFactory(ILogger<MockNetworkAdapterFactory> logger)
    {
        _logger = logger;
        _adapters = new List<INetworkAdapter>
        {
            new NetworkAdapterMock(_logger, TimeSpan.FromSeconds(10)),
            new NetworkAdapterMock(_logger, TimeSpan.FromSeconds(3)),
            new NetworkAdapterMock(_logger, TimeSpan.FromSeconds(1))
        };

    }
    public IEnumerable<INetworkAdapter> Create()
    {
        return _adapters;
    }
}
