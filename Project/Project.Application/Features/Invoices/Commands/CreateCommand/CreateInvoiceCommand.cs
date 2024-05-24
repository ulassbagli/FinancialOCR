using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Invoices.Dtos.BaseDto;
using Project.Application.Features.Invoices.Rules;
using Project.Application.Services.Repositories.Invoices;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Invoices.Commands.CreateCommand
{
    public class CreateInvoiceCommand : IRequest<BaseInvoiceDto>
    {
        public string UserId { get; set; }

        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, BaseInvoiceDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IInvoiceWriteRepository _invoiceWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly InvoiceBusinessRules _invoiceBusinessRules;

            public CreateInvoiceCommandHandler(IInvoiceWriteRepository invoiceWriteRepository, IMapper mapper,
                InvoiceBusinessRules invoiceBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _invoiceBusinessRules = invoiceBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _invoiceWriteRepository = invoiceWriteRepository;
            }

            public async Task<BaseInvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
            {
                User user = await _invoiceBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _invoiceBusinessRules.CheckIfInvoiceAlreadyExists(request.UserId);

                var mappedInvoice = _mapper.Map<Invoice>(request);
                var createdInvoice = await _invoiceWriteRepository.AddAsync(mappedInvoice);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseInvoiceDto>(createdInvoice);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.Invoice.ToString() } }
                });
            }
        }
    }
}
