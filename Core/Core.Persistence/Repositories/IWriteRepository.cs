namespace Core.Persistence.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : Entity
{
    Task<T> AddAsync(T entity, bool withSave = true);
    Task<List<T>> AddRangeAsync(List<T> entities, bool withSave = true);
    Task<T> HardRemove(T entity, bool withSave = true);
    Task<T> SoftRemove(T entity, bool withSave = true);
    Task<List<T>> HardRemoveRange(List<T> entities, bool withSave = true);
    Task<List<T>> SoftRemoveRange(List<T> entities, bool withSave = true);
    Task<T> HardRemoveAsync(string id, bool withSave = true);
    Task<T> SoftRemoveAsync(string id, bool withSave = true);
    Task<T> Update(T entity, bool withSave = true);
    Task<int> SaveAsync();
    Task UpdateAsync(T entity, bool withSave = true);
    Task DeleteAsync(T Entity);
}