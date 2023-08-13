using Serilog;
using Serilog.Events;

namespace Cleverbit.CodingCase.Api.Extensions;
internal static class LoggerExtension
{
    internal static void AddLogger(this IServiceCollection serviceCollection, IConfiguration configuration, WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }
}