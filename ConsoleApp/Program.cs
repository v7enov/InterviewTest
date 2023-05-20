using CommonLib.Abstractions;
using CommonLib.Events;
using CommonLib.Watchers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        var appHost = Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices).Build();
    }

    static void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISystemEventNotifier, SystemEventNotifier>();
        serviceCollection.AddHostedService<DiskSpaceWatcher>();
        serviceCollection.AddHostedService<EthernetAdaptersWatcher>();
    }
}