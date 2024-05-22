using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Features.Developers.Commands.CreateCommand;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Companies.Dtos.BaseDto;
using Project.Application.Features.Companies.Rules;
using Project.Application.Services.Repositories.Companies;
using Project.Application.Services.Repositories.Developers;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Commands.CreatedCommand
{
    public class CreateCompanyCommand  : IRequest<BaseCompanyDto>
    {
        public string UserId { get; set; }

        public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, BaseCompanyDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ICompanyWriteRepository _companyWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly CompanyBusinessRules _companyBusinessRules;

            public CreateCompanyCommandHandler(ICompanyWriteRepository companyWriteRepository, IMapper mapper,
                CompanyBusinessRules companyBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _companyBusinessRules = companyBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _companyWriteRepository = companyWriteRepository;
            }

            public async Task<BaseCompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
            {
                User user = await _companyBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _companyBusinessRules.CheckIfCompanyAlreadyExists(request.UserId);

                var mappedCompany = _mapper.Map<Company>(request);
                var createdCompany = await _companyWriteRepository.AddAsync(mappedCompany);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseCompanyDto>(createdCompany);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Company.ToString() } }
                });
            }
        }
    }
}
