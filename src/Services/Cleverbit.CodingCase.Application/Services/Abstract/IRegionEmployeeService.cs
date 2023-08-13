using Cleverbit.CodingCase.Domain.Models;

namespace Cleverbit.CodingCase.Application.Services.Abstract
{
    public interface IRegionEmployeeService
    {
        Task<IEnumerable<Employee>?> GetEmployeesInRegionAndDescendants(int regionId);
        Task<IEnumerable<Employee>?> GetEmployeesInRegionAndAncestors(int regionId);
    }
}