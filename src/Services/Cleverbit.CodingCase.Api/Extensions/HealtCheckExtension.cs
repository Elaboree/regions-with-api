using Cleverbit.CodingCase.Application.Constants;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;

namespace Cleverbit.CodingCase.Api.Extensions;

internal static class HealthCheckExtension
{
    private const string HealthCheckEndpoint = "/health";

    internal static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var cleverbitCodingCaseDbConnection = configuration.GetConnectionString(ConnectionString.CleverbitCodingCaseDbConnectionString);

        const string healthQuery = "SELECT 1;";

        services.AddHealthChecks().AddSqlServer(
                connectionString: cleverbitCodingCaseDbConnection,
                healthQuery: healthQuery,
                name: nameof(ConnectionString.CleverbitCodingCaseDbConnectionString),
                failureStatus: HealthStatus.Degraded,
                tags: new string[] { HealthCheckTag.Database, HealthCheckTag.SqlServer },
                timeout: TimeSpan.FromSeconds(30));
    }

    internal static void UseHealthCheck(this IApplicationBuilder app)
    {
        app.UseHealthChecks(HealthCheckEndpoint, new HealthCheckOptions
        {
            ResponseWriter = async (c, r) =>
            {
                c.Response.ContentType = MediaTypeNames.Application.Json;

                var result = JsonSerializer.Serialize(new
                {
                    status = r.Status.ToString(),
                    components = r.Entries.Select(e => new
                    {
                        key = e.Key,
                        value = e.Value.Status.ToString()
                    })
                });

                await c.Response.WriteAsync(result);
            }
        });
    }
}