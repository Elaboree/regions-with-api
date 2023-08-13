using Cleverbit.CodingCase.Application.Models;

namespace Cleverbit.CodingCase.Infrastructure.Services.Abstract;

public interface ISeedService
{
    Task SeedEmployees(IEnumerable<EmployeeCsvDto> employees);
    Task SeedRegions(IEnumerable<RegionCsvDto> regions);
}