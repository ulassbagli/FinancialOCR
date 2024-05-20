using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Framework: Entity
{
    public string Name { get; set; }
    public string? Version { get; set; }
    
    public Guid ProgrammingLanguageId { get; set; }
    public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

    public Framework()
    {
        
    }

    public Framework(string name, string? version, Guid programmingLanguageId)
    {
        Name = name;
        Version = version;
        ProgrammingLanguageId = programmingLanguageId;
    }

    public Framework(Guid id, Guid programmingLanguageId, string name, string? version) : base(id)
    {
        Name = name;
        Version = version;
        ProgrammingLanguageId = programmingLanguageId;
    }
}