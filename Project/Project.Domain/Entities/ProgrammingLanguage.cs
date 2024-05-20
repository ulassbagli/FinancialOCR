using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProgrammingLanguage: Entity
{
    public string Name { get; set; }
    public virtual ICollection<Framework> Frameworks { get; set; }

    public ProgrammingLanguage()
    {
    }

    public ProgrammingLanguage(Guid Id, string name) : this()
    {
        this.Id = Id;
        this.Name = name;
    }
}