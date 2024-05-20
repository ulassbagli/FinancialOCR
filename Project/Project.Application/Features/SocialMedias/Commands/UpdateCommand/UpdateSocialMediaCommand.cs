using Application.Features.SocialMedias.Dtos;
using Application.Features.SocialMedias.Dtos.BaseDto;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.SocialMedias;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SocialMedias.Commands.UpdateCommand;

public class UpdateSocialMediaCommand : IRequest<BaseSocialMediaDto>
{
    public string Id { get; set; }
    public string? DeveloperId { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    
    public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, BaseSocialMediaDto>
    {
        private readonly ISocialMediaWriteRepository _socialMediaWriteRepository;
        private readonly ISocialMediaReadRepository _socialMediaReadRepository;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;
        private readonly IMapper _mapper;
        
        public UpdateSocialMediaCommandHandler(IMapper mapper, ISocialMediaWriteRepository socialMediaWriteRepository, ISocialMediaReadRepository socialMediaReadRepository, SocialMediaBusinessRules socialMediaBusinessRules)
        {
            _socialMediaWriteRepository = socialMediaWriteRepository;
            _socialMediaReadRepository = socialMediaReadRepository;
            _socialMediaBusinessRules = socialMediaBusinessRules;
            _mapper = mapper;
        }
        
        public async Task<BaseSocialMediaDto> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var socialMediaToUpdate = await _socialMediaReadRepository.GetByIdAsync(request.Id);
            await _socialMediaBusinessRules
                .CheckIfSocialMediaDoesNotExists(socialMediaToUpdate);

            _mapper.Map(request, socialMediaToUpdate, typeof(UpdateSocialMediaCommand), typeof(SocialMedia));
            await _socialMediaBusinessRules.CheckIfSocialMediaURLIsNotUnique(socialMediaToUpdate.Url);
             await _socialMediaWriteRepository.Update(socialMediaToUpdate);
            return _mapper.Map<BaseSocialMediaDto>(socialMediaToUpdate);
        }
    }
}