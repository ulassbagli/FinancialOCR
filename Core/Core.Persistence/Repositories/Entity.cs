namespace Core.Persistence.Repositories;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool isDeleted { get; set; } = false;

    public Entity()
    {
        CreatedDate = DateTime.UtcNow;
        DeletedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }
    
    public Entity(Guid id) : this()
    {
        Id = id;
    }
}