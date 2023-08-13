using Cleverbit.CodingCase.Application.Models;
using Cleverbit.CodingCase.Infrastructure.Services.Abstract;
using Cleverbit.CodingCase.SeedWorker.Settings;
using Microsoft.Extensions.Options;

namespace Cleverbit.CodingCase.SeedWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly ICsvService csvService;
    private readonly ISeedService seedService;
    private readonly WorkerSettings settings;
    public Worker(ILogger<Worker> logger, ICsvService csvService, ISeedService seedService, IOptions<WorkerSettings> settings)
    {
        this.logger = logger;
        this.csvService = csvService;
        this.seedService = seedService;
        this.settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var csvFolderPath = GetCsvDocumentsFolderPath();

        string regionsPath = Path.Join(csvFolderPath, "regions.csv");
        string employeesPath = Path.Join(csvFolderPath, "employees.csv");

        var regions = csvService.GetData<RegionCsvDto>(regionsPath);
        var employees = csvService.GetData<EmployeeCsvDto>(employeesPath);

        await seedService.SeedRegions(regions);
        await seedService.SeedEmployees(employees);
    }

    private string GetCsvDocumentsFolderPath()
    {
        string? relativePath = settings.RelativePath;
        string? folderName = settings.FolderName;

        string fullPath = Path.Combine(relativePath, folderName);
        return fullPath;
    }
}