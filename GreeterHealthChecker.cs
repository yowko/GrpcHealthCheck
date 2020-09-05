using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcHealthCheck
{
    public class GreeterHealthChecker : IHealthCheck
    {
        private readonly Greeter.GreeterClient _client;
        public GreeterHealthChecker(Greeter.GreeterClient client)
        {
            _client = client;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //以下就是自訂檢查邏輯
            var reply = await _client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });
            if (reply.Message == "Hello GreeterClient")
            {
                return HealthCheckResult.Healthy("A healthy result.");
            }
            return HealthCheckResult.Unhealthy("An unhealthy result.");
        }
    }
}