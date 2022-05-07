using WorkerConsumoAPIContagem;
using WorkerConsumoAPIContagem.Resilience;
using Polly;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<AsyncPolicy>(
            WaitAndRetryExtensions.CreateWaitAndRetryPolicy(new []
            {
                TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(7)
            }));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();