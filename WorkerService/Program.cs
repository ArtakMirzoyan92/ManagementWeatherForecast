using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<OldDataCleanupWorker>();
    })
    .Build();

await host.RunAsync();
