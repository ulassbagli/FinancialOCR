using Application.Services.Repositories.OperationClaims;
using Core.Security.Entities;
using MediatR;
using Project.Application.Services.Repositories.AccountantsOperationClaims;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Authentication.Command.AccountantOperationClaimCommand
{
    public class CreateAccountantOperationClaimCommand : IRequest<List<AccountantOperationClaim>>
    {
        public Accountant Accountant { get; set; }
        public HashSet<OperationClaim> OperationClaims { get; set; }

        public class CreateAccountantOperationClaimCommandHandler : IRequestHandler<CreateAccountantOperationClaimCommand, List<AccountantOperationClaim>>
        {
            private readonly IAccountantOperationClaimWriteRepository _accountantOperationClaimWriteRepository;
            private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;

            public CreateAccountantOperationClaimCommandHandler(IAccountantOperationClaimWriteRepository accountantOperationClaimWriteRepository,
                IOperationClaimWriteRepository operationClaimWriteRepository)
            {
                _accountantOperationClaimWriteRepository = accountantOperationClaimWriteRepository;
                _operationClaimWriteRepository = operationClaimWriteRepository;
            }

            public async Task<List<AccountantOperationClaim>> Handle(CreateAccountantOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var accountantOperationClaimsToAdd = new List<AccountantOperationClaim>();

                await _operationClaimWriteRepository.AddRangeAsync(request.OperationClaims.ToList());

                foreach (var operationClaim in request.OperationClaims)
                {
                    accountantOperationClaimsToAdd.Add(new AccountantOperationClaim()
                    {
                        Accountant = request.Accountant,
                        OperationClaimId = operationClaim.Id
                    });
                }

                await _accountantOperationClaimWriteRepository.AddRangeAsync(accountantOperationClaimsToAdd);
                return accountantOperationClaimsToAdd;
            }
        }
    }
}
