using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Companies.Dtos.BaseDto;
using Project.Application.Features.Companies.Rules;
using Project.Application.Services.Repositories.Companies;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Commands.UpdateCommand
{
    public class UpdateCompanyCommand : IRequest<BaseCompanyDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, BaseCompanyDto>
        {
            private readonly ICompanyWriteRepository _companyWriteRepository;
            private readonly CompanyBusinessRules _companyBusinessRules;
            private readonly IMapper _mapper;

            public UpdateCompanyCommandHandler(IMapper mapper, ICompanyWriteRepository companyWriteRepository, CompanyBusinessRules companyBusinessRules)
            {
                _companyWriteRepository = companyWriteRepository;
                _companyBusinessRules = companyBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseCompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
            {
                var companyToUpdate = await _companyBusinessRules.CheckIfCompanyDoesNotExistsAndGetCompany(request.Id);
                await _companyBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);

                _mapper.Map(request, companyToUpdate, typeof(UpdateCompanyCommand), typeof(Company));
                await _companyWriteRepository.Update(companyToUpdate);
                return _mapper.Map<BaseCompanyDto>(companyToUpdate);
            }
        }
    }
}
