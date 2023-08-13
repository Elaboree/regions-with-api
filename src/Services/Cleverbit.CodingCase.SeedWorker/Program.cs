using Cleverbit.CodingCase.SeedWorker;
using Cleverbit.CodingCase.SeedWorker.Extensions;
using Cleverbit.CodingCase.SeedWorker.Settings;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        var configuration = hostContext.Configuration;

        services.Configure<WorkerSettings>(configuration.GetSection(nameof(WorkerSettings)));

        services.AddServiceProviders(configuration);

        services.AddSingleton<IConfiguration>(configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();