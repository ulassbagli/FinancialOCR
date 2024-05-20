using Application.Features.Developers.Dtos.BaseDto;

namespace Application.Features.SocialMedias.Dtos;

public class SocialMediaListDto
{
    public Guid Id { get; set; }
    public BaseDeveloperDto Developer { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}