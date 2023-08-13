using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Domain.Models;
using Cleverbit.CodingCase.Infrastructure.Context;

namespace Cleverbit.CodingCase.Infrastructure.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(CodingCaseDbContext dbContext) : base(dbContext)
    {

    }
}