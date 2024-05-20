using Application.Features.SocialMedias.Dtos.BaseDto;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories.SocialMedias;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SocialMedias.Commands.CreateCommand;

public class CreateSocialMediaCommand : IRequest<BaseSocialMediaDto>
{
    public Guid DeveloperId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    
    public class CreateSocialMediaCommandHandler:IRequestHandler<CreateSocialMediaCommand, BaseSocialMediaDto>
    {
        private readonly ISocialMediaWriteRepository _socialMediaWriteRepository;
        private readonly IMapper _mapper;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

        public CreateSocialMediaCommandHandler(ISocialMediaWriteRepository socialMediaWriteRepository, IMapper mapper, SocialMediaBusinessRules socialMediaBusinessRules)
        {
            _mapper = mapper;
            _socialMediaBusinessRules = socialMediaBusinessRules;
            _socialMediaWriteRepository = socialMediaWriteRepository;
        }

        public async Task<BaseSocialMediaDto> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            await _socialMediaBusinessRules.CheckIfSocialMediaURLIsNotUnique(request.Url);
            
            var mappedSocialMedia = _mapper.Map<SocialMedia>(request);
            var createdSocialMedia = await _socialMediaWriteRepository.AddAsync(mappedSocialMedia);
            return _mapper.Map<BaseSocialMediaDto>(createdSocialMedia);
        }
    }
}