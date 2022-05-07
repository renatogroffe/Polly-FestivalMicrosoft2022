using System.Text.Json;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;

namespace WorkerConsumoAPIContagem.Resilience;

public static class MonkeyPolicyContagem
{
    public static AsyncInjectOutcomePolicy CreateMonkeyPolicy(
        IConfiguration configuration)
    {
        // Criação da Chaos Policy com a probabilidade
        // definida no appsettings.json
        return MonkeyPolicy.InjectExceptionAsync(
            with => with.Fault(new Exception("Erro gerado em simulação de caos com Simmy..."))
                .InjectionRate(JsonSerializer.Deserialize<double>(
                    configuration["ChaosEngineering:InjectionRate"]))
                .Enabled(Convert.ToBoolean(
                    configuration["ChaosEngineering:Simulate"])));
    }
}