using Cleverbit.CodingCase.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cleverbit.CodingCase.Infrastructure.EntityConfigurations.Employee;

public class EmployeeEntityConfiguration : BaseEntityConfiguration<Domain.Models.Employee>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.Employee> builder)
    {
        base.Configure(builder);

        builder.ToTable("employees", CodingCaseDbContext.DEFAULT_SCHEMA);

        builder.HasKey(employee => employee.Id);
    }
}
