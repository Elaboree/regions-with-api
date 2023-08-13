using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Application.Services.Abstract;
using Cleverbit.CodingCase.Domain.Models;

namespace Cleverbit.CodingCase.Application.Services;

public class RegionEmployeeService : IRegionEmployeeService
{
    private readonly IRegionRepository regionRepository;
    public RegionEmployeeService(IRegionRepository regionRepository)
    {
        this.regionRepository = regionRepository;
    }

    public async Task<IEnumerable<Employee>?> GetEmployeesInRegionAndDescendants(int regionId)
    {
        var currentRegion = await regionRepository.GetByIdAsync(regionId, employees => employees.Employees);

        if (currentRegion is null) return null;

        var regionEmployees = new List<Employee>();
        regionEmployees.AddRange(currentRegion.Employees);

        await FindRegionDescendants(currentRegion, regionEmployees);
        return regionEmployees;
    }

    public async Task<IEnumerable<Employee>?> GetEmployeesInRegionAndAncestors(int regionId)
    {
        var currentRegion = await regionRepository.GetByIdAsync(regionId, employees => employees.Employees);

        if (currentRegion is null) return null;

        var regionEmployees = new List<Employee>();
        regionEmployees.AddRange(currentRegion.Employees);

        await FindRegionAncestors(currentRegion.Id, regionEmployees);
        return regionEmployees;
    }

    private async Task FindRegionAncestors(int regionId, List<Employee> regionEmployees)
    {
        var parentRegion = await regionRepository.GetByIdAsync(regionId, region => region.Employees);

        if (parentRegion is null) return;

        regionEmployees.AddRange(parentRegion.Employees);

        if (parentRegion.ParentId.HasValue)
        {
            await FindRegionAncestors(parentRegion.ParentId.Value, regionEmployees);
        }
    }
    private async Task FindRegionDescendants(Region region, List<Employee> regionEmployees)
    {
        var regionQueue = new Queue<Region>();

        // Enqueue the starting region
        regionQueue.Enqueue(region);

        while (regionQueue.Count > 0)
        {
            Region currentRegion = regionQueue.Dequeue();

            var regionChildrenList = await regionRepository.GetList(region => region.ParentId == currentRegion.Id,
                                           regionEmployees => regionEmployees.Employees);

            foreach (var childRegion in regionChildrenList)
            {
                regionEmployees.AddRange(childRegion.Employees);
                regionQueue.Enqueue(childRegion);
            }
        }
    }
}