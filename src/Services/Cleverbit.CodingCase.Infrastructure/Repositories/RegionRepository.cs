using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Domain.Models;
using Cleverbit.CodingCase.Infrastructure.Context;

namespace Cleverbit.CodingCase.Infrastructure.Repositories;

public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    public RegionRepository(CodingCaseDbContext dbContext) : base(dbContext)
    {
    }
}
