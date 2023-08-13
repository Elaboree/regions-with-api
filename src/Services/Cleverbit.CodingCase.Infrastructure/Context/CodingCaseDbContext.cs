using Cleverbit.CodingCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cleverbit.CodingCase.Infrastructure.Context;

public class CodingCaseDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public CodingCaseDbContext()
    {

    }

    public CodingCaseDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //For the migration Configuring, Compile Time
        if (!optionsBuilder.IsConfigured)
        {
            var connStr = "Server=.;Database=CleverbitCodingCase;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        OnBeforeSaveOrUpdate();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaveOrUpdate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaveOrUpdate();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveOrUpdate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaveOrUpdate()
    {
        var addedEntites = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);

        var updatedEntites = ChangeTracker.Entries()
         .Where(i => i.State == EntityState.Modified)
         .Select(i => (BaseEntity)i.Entity);

        PrepareUpdatedEntites(updatedEntites);
        PrepareAddedEntites(addedEntites);
    }

    private void PrepareAddedEntites(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
        }
    }

    private void PrepareUpdatedEntites(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.ModifiedDate = DateTime.Now;
        }
    }
}
