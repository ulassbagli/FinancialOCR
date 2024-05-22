using Application.Features.Companys.Commands.DeleteCommand;
using Application.Features.Companys.Dtos;
using Application.Features.Companys.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Companies.Dtos;
using Project.Application.Features.Companies.Rules;
using Project.Application.Services.Repositories.Companies;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Companies.Commands.DeletedCommand
{
    public class DeleteCompanyCommand : IRequest<DeletedCompanyDto>
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, DeletedCompanyDto>
        {
            private readonly ICompanyWriteRepository _companyWriteRepository;
            private readonly IMapper _mapper;
            private readonly CompanyBusinessRules _companyBusinessRules;

            public DeleteCompanyCommandHandler(ICompanyWriteRepository companyWriteRepository, IMapper mapper,
                CompanyBusinessRules companyBusinessRules)
            {
                _mapper = mapper;
                _companyBusinessRules = companyBusinessRules;
                _companyWriteRepository = companyWriteRepository;
            }

            public async Task<DeletedCompanyDto> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
            {
                var company = await _companyBusinessRules.CheckIfCompanyDoesNotExistsAndGetCompany(request.Id);

                Company deletedCompany;
                if (request.isSoftDelete)
                    deletedCompany = await _companyWriteRepository.SoftRemove(company);
                else
                    deletedCompany = await _companyWriteRepository.HardRemove(company);

                return _mapper.Map<DeletedCompanyDto>(deletedCompany);
            }
        }
    }
}
