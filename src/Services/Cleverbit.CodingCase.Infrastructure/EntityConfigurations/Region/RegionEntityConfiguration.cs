using Cleverbit.CodingCase.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cleverbit.CodingCase.Infrastructure.EntityConfigurations.Region;

public class RegionEntityConfiguration : BaseEntityConfiguration<Domain.Models.Region>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.Region> builder)
    {
        base.Configure(builder);

        builder.ToTable("regions", CodingCaseDbContext.DEFAULT_SCHEMA);

        builder.HasKey(region => region.Id);

        builder.Property(e=> e.Id).ValueGeneratedNever();

        // Define self-referencing foreign key relationship
        builder.HasOne(region => region.Parent)
            .WithMany()
            .HasForeignKey(region => region.ParentId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}