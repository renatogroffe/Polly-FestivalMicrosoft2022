using WorkerConsumoAPIContagem;
using WorkerConsumoAPIContagem.Resilience;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(
            FallbackContagem.CreateFallbackPolicy().WrapAsync(
                MonkeyPolicyContagem.CreateMonkeyPolicy(context.Configuration)));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();