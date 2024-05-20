using Application.Features.SocialMedias.Models;
using Application.Services.Repositories;
using Application.Services.Repositories.SocialMedias;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SocialMedias.Queries;

public class GetListSocialMediaQuery: IRequest<SocialMediaListModel>
{
    public PageRequest PageRequest { get; set; }
    
    public class GetListSocialMediaQueryHandler : IRequestHandler<GetListSocialMediaQuery, SocialMediaListModel>
    {
        private readonly ISocialMediaReadRepository _socialMediaReadRepository;
        private readonly IMapper _mapper;

        public GetListSocialMediaQueryHandler(ISocialMediaReadRepository socialMediaReadRepository, IMapper mapper)
        {
            _socialMediaReadRepository = socialMediaReadRepository;
            _mapper = mapper;
        }

        public async Task<SocialMediaListModel> Handle(GetListSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var socialMedias = await _socialMediaReadRepository.GetListAsync(include: m=>m.Include(p=>p.Developer),
                index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            return _mapper.Map<SocialMediaListModel>(socialMedias);
        }
    }
}