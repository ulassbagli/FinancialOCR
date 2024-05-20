using System.Linq.Expressions;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public class ReadRepository<T, TContext> : IReadRepository<T> 
    where T : Entity
    where TContext : DbContext
{
    protected TContext Context { get; }
    public ReadRepository(TContext context)
    {
        Context = context;
    }
    public DbSet<T> Table => Context.Set<T>();
    public IQueryable<T> Query() => Table;

    public async Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0,
        int size = 10, bool tracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<T> queryable = Query();
        if (!tracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<IPaginate<T>> GetListByDynamicAsync(Dynamic.Dynamic dynamic, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> queryable = Query().ToDynamic(dynamic);
        if (!tracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool tracking = true)
    {
        var queryable = Query();
        if (!tracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var queryable = Query();
            if (!tracking)
                queryable = queryable.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }
    }
}