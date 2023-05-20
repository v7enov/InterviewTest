﻿using CommonLib.Abstractions;
using CommonLib.Events;
using CommonLib.Models.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommonLib.Watchers;

public class DiskSpaceWatcher : BackgroundService
{
    private readonly ILogger<DiskSpaceWatcher> _logger;
    private readonly ISystemEventNotifier _systemEventNotifier;
    private readonly IDiskInfoFactory _diskInfoFactory;

    public DiskSpaceWatcher(
        ILogger<DiskSpaceWatcher> logger, 
        ISystemEventNotifier systemEventNotifier,
        IDiskInfoFactory diskInfoFactory
        )
    {
        _logger = logger;
        _systemEventNotifier = systemEventNotifier;
        _diskInfoFactory = diskInfoFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //TODO:
        await Task.CompletedTask;
    }
}

