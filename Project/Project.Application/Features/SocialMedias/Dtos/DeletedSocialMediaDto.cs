using Application.Features.SocialMedias.Dtos.BaseDto;

namespace Application.Features.SocialMedias.Dtos;

public class DeletedSocialMediaDto : BaseSocialMediaDto
{
    public DateTime DeletedDate { get; set; }
    public bool isDeleted { get; set; }
}