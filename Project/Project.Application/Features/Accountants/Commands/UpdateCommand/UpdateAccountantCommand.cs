using Application.Features.Developers.Commands.UpdateCommand;
using Application.Features.Developers.Dtos.BaseDto;
using Application.Features.Developers.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Accountants.Dtos.BaseDto;
using Project.Application.Features.Accountants.Rules;
using Project.Application.Services.Repositories.Accountants;
using Project.Application.Services.Repositories.Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Accountants.Commands.UpdateCommand
{
    public class UpdateAccountantCommand : IRequest<BaseAccountantDto>
    {
        public string Id { get; set; }
        public string AccountantId { get; set; }

        public class UpdateAccountantCommandHandler : IRequestHandler<UpdateAccountantCommand, BaseAccountantDto>
        {
            private readonly IAccountantWriteRepository _accountantWriteRepository;
            private readonly AccountantBusinessRules _accountantBusinessRules;
            private readonly IMapper _mapper;

            public UpdateAccountantCommandHandler(IMapper mapper, IAccountantWriteRepository accountantWriteRepository, AccountantBusinessRules accountantBusinessRules)
            {
                _accountantWriteRepository = accountantWriteRepository;
                _accountantBusinessRules = accountantBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseAccountantDto> Handle(UpdateAccountantCommand request, CancellationToken cancellationToken)
            {
                var accountantToUpdate = await _accountantBusinessRules.CheckIfAccountantDoesNotExistsAndGetAccountant(request.Id);
                await _accountantBusinessRules.CheckIfAccountantDoesNotExistsAndGetAccountant(request.AccountantId);

                _mapper.Map(request, accountantToUpdate, typeof(UpdateDeveloperCommand), typeof(Developer));
                await _accountantWriteRepository.Update(accountantToUpdate);
                return _mapper.Map<BaseAccountantDto>(accountantToUpdate);
            }
        }
    }
}
