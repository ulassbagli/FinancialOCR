using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Developer : Entity
{
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<SocialMedia> SocialMedias { get; set; }
    
    public Developer()
    {
    }
    
    public Developer(Guid id, Guid userId):base(id)
    {
        UserId = userId;
    }
}