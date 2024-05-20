using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Persistence.Repositories;

public class WriteRepository<T, TContext> : IWriteRepository<T> 
    where T : Entity
    where TContext : DbContext
{
    protected TContext Context { get; }
    public WriteRepository(TContext context)
    {
        Context = context;
    }
    public DbSet<T> Table => Context.Set<T>();
    public IQueryable<T> Query() => Table;

    public async Task<T> AddAsync(T entity, bool withSave = true)
    {
        EntityEntry<T> entry = await Table.AddAsync(entity);
        if (withSave)
        {
            await SaveAsync();
        }
        return entry.Entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> entities, bool withSave)
    {
        await Table.AddRangeAsync(entities);
        if (withSave)
        {
            await SaveAsync();
        }
        return entities;
    }

    public async Task<T> SoftRemove(T entity, bool withSave = true)
    {
        entity.isDeleted = true;
        entity.DeletedDate = DateTime.Now;
        
        EntityEntry<T> entry = Table.Update(entity);
        if (withSave)
        {
            await SaveAsync();
        }
        return entry.Entity;
    }

    public async Task<List<T>> SoftRemoveRange(List<T> entities, bool withSave = true)
    {
        entities.ForEach(entity=>
        {
            entity.isDeleted = true;
            entity.DeletedDate = DateTime.Now;
        });

        Table.UpdateRange(entities);
        if (withSave)
        {
            await SaveAsync();
        }
        return entities;
    }

    public async Task<T> SoftRemoveAsync(string id, bool withSave = true)
    {
        T entity = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return await SoftRemove(entity, withSave);
    }

    public async Task<T> HardRemove(T entity, bool withSave = true)
    {
        EntityEntry<T> entry = Table.Remove(entity);
        if (withSave)
        {
            await SaveAsync();
        }
        return entry.Entity;
    }

    public async Task<List<T>> HardRemoveRange(List<T> entities, bool withSave = true)
    {
        Table.RemoveRange(entities);
        if (withSave)
        {
            await SaveAsync();
        }
        return entities;
    }

    public async Task<T> HardRemoveAsync(string id, bool withSave)
    {
        T entity = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return await HardRemove(entity, withSave);
    }

    public async Task<T> Update(T entity, bool withSave = true)
    {
        EntityEntry<T> entry = Table.Update(entity);
        if (withSave)
        {
            await SaveAsync();
        }
        return entry.Entity;
    }

    public async Task<int> SaveAsync()
        => await Context.SaveChangesAsync();
}