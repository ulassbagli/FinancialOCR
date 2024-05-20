using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories;

public interface IRepository<T> : IQuery<T> where T : Entity
{
    DbSet<T> Table { get; }
}