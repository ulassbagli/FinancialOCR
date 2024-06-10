using Application.Services.Repositories.OperationClaims;
using Core.Security.Entities;
using MediatR;
using Project.Application.Features.Authentication.Command.CustomerOperationClaimCommand;
using Project.Application.Services.Repositories.CustomerOperationClaims;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Authentication.Command.CustomerOperationClaimCommand
{
    public class CreateCustomerOperationClaimCommand : IRequest<List<CustomerOperationClaim>>
    {
        public Customer Customer { get; set; }
        public HashSet<OperationClaim> OperationClaims { get; set; }

        public class CreateCustomerOperationClaimCommandHandler : IRequestHandler<CreateCustomerOperationClaimCommand, List<CustomerOperationClaim>>
        {
            private readonly ICustomerOperationClaimWriteRepository _customerOperationClaimWriteRepository;
            private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;

            public CreateCustomerOperationClaimCommandHandler(ICustomerOperationClaimWriteRepository customerOperationClaimWriteRepository,
                IOperationClaimWriteRepository operationClaimWriteRepository)
            {
                _customerOperationClaimWriteRepository = customerOperationClaimWriteRepository;
                _operationClaimWriteRepository = operationClaimWriteRepository;
            }

            public async Task<List<CustomerOperationClaim>> Handle(CreateCustomerOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var customerOperationClaimsToAdd = new List<CustomerOperationClaim>();

                await _operationClaimWriteRepository.AddRangeAsync(request.OperationClaims.ToList());

                foreach (var operationClaim in request.OperationClaims)
                {
                    customerOperationClaimsToAdd.Add(new CustomerOperationClaim()
                    {
                        Customer = request.Customer,
                        OperationClaimId = operationClaim.Id
                    });
                }

                await _customerOperationClaimWriteRepository.AddRangeAsync(customerOperationClaimsToAdd);
                return customerOperationClaimsToAdd;
            }
        }
    }
}
