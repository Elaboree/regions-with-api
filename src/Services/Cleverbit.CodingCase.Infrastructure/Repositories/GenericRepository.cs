using Cleverbit.CodingCase.Application.Interfaces.Repositories;
using Cleverbit.CodingCase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cleverbit.CodingCase.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext dbContext;

    protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

    public GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region Insert Methods
    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        return await dbContext.SaveChangesAsync();
    }
    public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
    {

        if (entities is not null && !entities.Any())
            return 0;

        await entity.AddRangeAsync(entities);
        return await dbContext.SaveChangesAsync();
    }

    #endregion

    #region Update Methods
    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return await dbContext.SaveChangesAsync();

    }
    #endregion

    #region Delete Methods
    public virtual Task<int> DeleteAsync(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);
        return dbContext.SaveChangesAsync();
    }
    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChanges() > 0;
    }
    #endregion


    #region Get Methods
    public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();
    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        return query;
    }

    public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        return await query.ToListAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await entity.ToListAsync();
    }
    public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity found = await entity.FindAsync(id);

        if (found is null)
            return null;


        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            if (include.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                Type propertyType = memberExpression.Type;

                // Check if the property is a collection navigation property
                if (propertyType.IsGenericType && typeof(ICollection<>).IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                {
                    dbContext.Entry(found).Collection(propertyName).Load();
                }
                else
                {
                    dbContext.Entry(found).Reference(propertyName).Load();
                }
            }
        }

        dbContext.Entry(found).State = EntityState.Detached;

        return found;
    }
    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate is not null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync();
    }

    #endregion

    #region SaveChanges Methods
    public Task<int> SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
    #endregion

    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {

        if (includes is not null)
        {
            foreach (var includeItem in includes)
            {
                query = query.Include(includeItem);
            }
        }
        return query;
    }

}
