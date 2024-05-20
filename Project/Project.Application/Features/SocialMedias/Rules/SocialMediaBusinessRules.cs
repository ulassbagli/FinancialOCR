using Application.Features.SocialMedias.Constants;
using Application.Services.Repositories.SocialMedias;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.SocialMedias.Rules;

public class SocialMediaBusinessRules
{
    private readonly ISocialMediaReadRepository _socialMediaReadRepository;

    public SocialMediaBusinessRules(ISocialMediaReadRepository socialMediaReadRepository)
    {
        _socialMediaReadRepository = socialMediaReadRepository;
    }
    
    public async Task CheckIfSocialMediaURLIsNotUnique(string url)
    {
        IPaginate<SocialMedia> result = await _socialMediaReadRepository.GetListAsync(b => b.Url == url);
        if (result.Items.Any()) throw new BusinessException(SocialMediaMessages.SocialMediaUrlAlreadyExists); //TODO: Localize message.
    }
    public async Task CheckIfSocialMediaDoesNotExists(SocialMedia socialMedia)
    {
        if (socialMedia == null) throw new BusinessException(SocialMediaMessages.SocialMediaNotFound); //TODO: Localize message.
    }
}