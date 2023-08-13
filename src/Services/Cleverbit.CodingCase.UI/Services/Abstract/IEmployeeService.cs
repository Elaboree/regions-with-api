using Cleverbit.CodingCase.UI.Models;

namespace Cleverbit.CodingCase.UI.Services.Abstract;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeItem>> GetEmployeesAsync();
}