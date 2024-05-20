using Core.Persistence.Repositories;

namespace Domain.Entities;
public class SocialMedia : Entity
{
    public Guid DeveloperId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    
    public virtual Developer Developer { get; set; }

    public SocialMedia()
    {
        
    }

    public SocialMedia(Guid id, Guid developerId, string name, string url) : base(id)
    {
        DeveloperId = developerId;
        Name = name;
        Url = url;
    }
}