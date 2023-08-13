using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Infrastructure.Context;
using Cleverbit.CodingCase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cleverbit.CodingCase.Infrastructure.Extensions;

public static class Registiration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString(ConnectionString.CleverbitCodingCaseDbConnectionString);

        services.AddDbContext<CodingCaseDbContext>(configuration =>
        {
            configuration.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });

            configuration.EnableSensitiveDataLogging();
            configuration.EnableDetailedErrors();
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        return services;
    }
}
