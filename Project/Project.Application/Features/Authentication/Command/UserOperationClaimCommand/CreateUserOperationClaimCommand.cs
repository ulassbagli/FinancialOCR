using Application.Services.Repositories.OperationClaims;
using Application.Services.Repositories.UserOperationClaims;
using Core.Security.Entities;
using MediatR;
using Project.Domain.Entities;

namespace Application.Features.Authentication.Command.UserOperationClaimCommand;

public class CreateUserOperationClaimCommand : IRequest<List<UserOperationClaim>>
{
    public User User { get; set; }
    public HashSet<OperationClaim> OperationClaims { get; set; }

    public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, List<UserOperationClaim>>
    {
        private readonly IUserOperationClaimWriteRepository _userOperationClaimWriteRepository;
        private readonly IOperationClaimWriteRepository _operationClaimWriteRepository;

        public CreateUserOperationClaimCommandHandler(IUserOperationClaimWriteRepository userOperationClaimWriteRepository, 
            IOperationClaimWriteRepository operationClaimWriteRepository)
        {
            _userOperationClaimWriteRepository = userOperationClaimWriteRepository;
            _operationClaimWriteRepository = operationClaimWriteRepository;
        }

        public async Task<List<UserOperationClaim>> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var userOperationClaimsToAdd = new List<UserOperationClaim>();

            await _operationClaimWriteRepository.AddRangeAsync(request.OperationClaims.ToList());

            foreach (var operationClaim in request.OperationClaims)
            {
                userOperationClaimsToAdd.Add(new UserOperationClaim()
                {
                    User = request.User,
                    OperationClaimId = operationClaim.Id
                });
            }

            await _userOperationClaimWriteRepository.AddRangeAsync(userOperationClaimsToAdd);
            return userOperationClaimsToAdd;
        }
    }
}