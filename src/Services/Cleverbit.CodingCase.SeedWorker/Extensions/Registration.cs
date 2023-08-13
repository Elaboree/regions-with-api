using Cleverbit.CodingCase.Application.Constants;
using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Infrastructure.Context;
using Cleverbit.CodingCase.Infrastructure.Repositories;
using Cleverbit.CodingCase.Infrastructure.Services;
using Cleverbit.CodingCase.Infrastructure.Services.Abstract;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingCase.SeedWorker.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddServiceProviders(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString(ConnectionString.CleverbitCodingCaseDbConnectionString);

            services.AddDbContext<CodingCaseDbContext>(opts =>
                    opts.UseSqlServer(connStr, opt => opt.EnableRetryOnFailure()), ServiceLifetime.Singleton);


            services.AddSingleton<IFactory, Factory>();
            services.AddSingleton<ICsvService, CsvService>();
            services.AddSingleton<ISeedService, SeedService>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IRegionRepository, RegionRepository>();


            return services;
        }
    }
}
