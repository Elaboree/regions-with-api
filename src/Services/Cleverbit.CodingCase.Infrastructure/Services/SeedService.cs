using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Application.Models;
using Cleverbit.CodingCase.Domain.Models;
using Cleverbit.CodingCase.Infrastructure.Services.Abstract;

namespace Cleverbit.CodingCase.Infrastructure.Services
{
    public class SeedService : ISeedService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IRegionRepository regionRepository;

        public SeedService(IEmployeeRepository employeeRepository, IRegionRepository regionRepository)
        {
            this.employeeRepository = employeeRepository;
            this.regionRepository = regionRepository;
        }


        public async Task SeedEmployees(IEnumerable<EmployeeCsvDto> employees)
        {
            await employeeRepository.AddRangeAsync(employees.Select(x => new Employee
            {
                Name = x.Name,
                Surname = x.Surname,
                RegionId = x.RegionId
            }));
        }

        public Task SeedRegions(IEnumerable<RegionCsvDto> regions)
        {
            return regionRepository.AddRangeAsync(regions.Select(x => new Region
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId
            }));
        }
    }
}
