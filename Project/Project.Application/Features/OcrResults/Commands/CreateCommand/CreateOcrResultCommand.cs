using Application.Features.Authentication.Command.UserOperationClaimCommand;
using Application.Features.OcrResults.Commands.CreateCommand;
using Application.Features.OcrResults.Dtos.BaseDto;
using Application.Features.OcrResults.Rules;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;
using Project.Application.Features.OcrResults.Dtos.BaseDto;
using Project.Application.Features.OcrResults.Rules;
using Project.Application.Services.Repositories.OcrResults;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Commands.CreateCommand
{
    public class CreateOcrResultCommand : IRequest<BaseOcrResultDto>
    {
        public string UserId { get; set; }

        public class CreateOcrResultCommandHandler : IRequestHandler<CreateOcrResultCommand, BaseOcrResultDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IOcrResultWriteRepository _ocrResultWriteRepository;
            private readonly IUserReadRepository _userReadRepository;
            private readonly OcrResultBusinessRules _ocrResultBusinessRules;

            public CreateOcrResultCommandHandler(IOcrResultWriteRepository ocrResultWriteRepository, IMapper mapper,
                OcrResultBusinessRules ocrResultBusinessRules, IMediator mediator, IUserReadRepository userReadRepository)
            {
                _mapper = mapper;
                _ocrResultBusinessRules = ocrResultBusinessRules;
                _mediator = mediator;
                _userReadRepository = userReadRepository;
                _ocrResultWriteRepository = ocrResultWriteRepository;
            }

            public async Task<BaseOcrResultDto> Handle(CreateOcrResultCommand request, CancellationToken cancellationToken)
            {
                User user = await _ocrResultBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);
                await _ocrResultBusinessRules.CheckIfOcrResultAlreadyExists(request.UserId);

                var mappedOcrResult = _mapper.Map<OcrResult>(request);
                var createdOcrResult = await _ocrResultWriteRepository.AddAsync(mappedOcrResult);
                await AddRoleToUserAsync(user);
                return _mapper.Map<BaseOcrResultDto>(createdOcrResult);
            }

            private async Task AddRoleToUserAsync(User userToAdd)
            {
                await _mediator.Send(new CreateUserOperationClaimCommand
                {
                    User = userToAdd,
                    OperationClaims = new HashSet<OperationClaim>() { new() { Name = OperationClaimsEnum.OcrResult.ToString() } }
                });
            }
        }
    }
}
