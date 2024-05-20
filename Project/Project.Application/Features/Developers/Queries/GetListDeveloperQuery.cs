using Application.Features.Developers.Models;
using Application.Services.Repositories;
using Application.Services.Repositories.Developers;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Developers.Queries;

public class GetListDeveloperQuery: IRequest<DeveloperListModel>
{
    public PageRequest PageRequest { get; set; }
    
    public class GetListDeveloperQueryHandler : IRequestHandler<GetListDeveloperQuery, DeveloperListModel>
    {
        private readonly IMapper _mapper;
        private readonly IDeveloperReadRepository _developerReadRepository;

        public GetListDeveloperQueryHandler(IDeveloperReadRepository developerReadRepository, IMapper mapper)
        {
            _mapper = mapper;
            _developerReadRepository = developerReadRepository;
        }

        public async Task<DeveloperListModel> Handle(GetListDeveloperQuery request, CancellationToken cancellationToken)
        {
            var developers = await _developerReadRepository.GetListAsync(include: m=>m.Include(p=>p.User),
                index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            return _mapper.Map<DeveloperListModel>(developers);
        }
    }
}