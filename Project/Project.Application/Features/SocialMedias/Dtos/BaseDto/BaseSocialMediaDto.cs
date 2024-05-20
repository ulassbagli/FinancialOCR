namespace Application.Features.SocialMedias.Dtos.BaseDto;

public class BaseSocialMediaDto
{
    public Guid Id { get; set; }
    public Guid DeveloperId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}