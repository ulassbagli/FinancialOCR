using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Accountants.Dtos;
using Project.Application.Features.Accountants.Rules;
using Project.Application.Services.Repositories.Accountants;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Commands.DeleteCommand;

public class DeleteAccountantCommand : IRequest<DeletedAccountantDto>
{
    public string Id { get; set; }
    public bool isSoftDelete { get; set; }

    public class DeleteAccountantCommandHandler : IRequestHandler<DeleteAccountantCommand, DeletedAccountantDto>
    {
        private readonly IAccountantWriteRepository _accountantWriteRepository;
        private readonly IMapper _mapper;
        private readonly AccountantBusinessRules _accountantBusinessRules;

        public DeleteAccountantCommandHandler(IAccountantWriteRepository accountantWriteRepository, IMapper mapper,
            AccountantBusinessRules accountantBusinessRules)
        {
            _mapper = mapper;
            _accountantBusinessRules = accountantBusinessRules;
            _accountantWriteRepository = accountantWriteRepository;
        }

        public async Task<DeletedAccountantDto> Handle(DeleteAccountantCommand request, CancellationToken cancellationToken)
        {
            var accountant = await _accountantBusinessRules.CheckIfAccountantDoesNotExistsAndGetAccountant(request.Id);

            Accountant deletedAccountant;
            if (request.isSoftDelete)
                deletedAccountant = await _accountantWriteRepository.SoftRemove(accountant);
            else
                deletedAccountant = await _accountantWriteRepository.HardRemove(accountant);

            return _mapper.Map<DeletedAccountantDto>(deletedAccountant);
        }
    }
}
