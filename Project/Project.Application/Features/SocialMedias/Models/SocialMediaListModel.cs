using Application.Features.SocialMedias.Dtos.BaseDto;
using Core.Persistence.Paging;

namespace Application.Features.SocialMedias.Models;

public class SocialMediaListModel : BasePageableModel 
{
    public IEnumerable<BaseSocialMediaDto> Items { get; set; }
}