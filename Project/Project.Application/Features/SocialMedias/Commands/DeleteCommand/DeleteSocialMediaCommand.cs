using Application.Features.SocialMedias.Dtos;
using Application.Features.SocialMedias.Rules;
using Application.Services.Repositories;
using Application.Services.Repositories.SocialMedias;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.SocialMedias.Commands.DeleteCommand;

public class DeleteSocialMediaCommand : IRequest<DeletedSocialMediaDto>
{
    public string Id { get; set; }
    public bool isSoftDelete { get; set; }
    
    public class DeleteSocialMediaCommandHandler:IRequestHandler<DeleteSocialMediaCommand, DeletedSocialMediaDto>
    {
        private readonly ISocialMediaWriteRepository _socialMediaWriteRepository;
        private readonly ISocialMediaReadRepository _socialMediaReadRepository;
        private readonly IMapper _mapper;
        private readonly SocialMediaBusinessRules _socialMediaBusinessRules;

        public DeleteSocialMediaCommandHandler(ISocialMediaWriteRepository socialMediaWriteRepository, IMapper mapper, SocialMediaBusinessRules socialMediaBusinessRules, ISocialMediaReadRepository socialMediaReadRepository)
        {
            _mapper = mapper;
            _socialMediaBusinessRules = socialMediaBusinessRules;
            _socialMediaReadRepository = socialMediaReadRepository;
            _socialMediaWriteRepository = socialMediaWriteRepository;
        }

        public async Task<DeletedSocialMediaDto> Handle(DeleteSocialMediaCommand request, CancellationToken cancellationToken)
        {
            var socialMedia = await _socialMediaReadRepository.GetByIdAsync(request.Id);
            await _socialMediaBusinessRules.CheckIfSocialMediaDoesNotExists(socialMedia);

            SocialMedia deletedSocialMedia;
            if (request.isSoftDelete)
                deletedSocialMedia = await _socialMediaWriteRepository.SoftRemove(socialMedia);
            else
                deletedSocialMedia = await _socialMediaWriteRepository.HardRemove(socialMedia);
            
            return _mapper.Map<DeletedSocialMediaDto>(deletedSocialMedia);
        }
    }
}