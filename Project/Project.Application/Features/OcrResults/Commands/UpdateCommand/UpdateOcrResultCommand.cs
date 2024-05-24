using AutoMapper;
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

namespace Project.Application.Features.OcrResults.Commands.UpdateCommand
{
    public class UpdateOcrResultCommand : IRequest<BaseOcrResultDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateOcrResultCommandHandler : IRequestHandler<UpdateOcrResultCommand, BaseOcrResultDto>
        {
            private readonly IOcrResultWriteRepository _ocrResultWriteRepository;
            private readonly OcrResultBusinessRules _ocrResultBusinessRules;
            private readonly IMapper _mapper;

            public UpdateOcrResultCommandHandler(IMapper mapper, IOcrResultWriteRepository ocrResultWriteRepository, OcrResultBusinessRules ocrResultBusinessRules)
            {
                _ocrResultWriteRepository = ocrResultWriteRepository;
                _ocrResultBusinessRules = ocrResultBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseOcrResultDto> Handle(UpdateOcrResultCommand request, CancellationToken cancellationToken)
            {
                var ocrResultToUpdate = await _ocrResultBusinessRules.CheckIfOcrResultDoesNotExistsAndGetOcrResult(request.Id);
                await _ocrResultBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);

                _mapper.Map(request, ocrResultToUpdate, typeof(UpdateOcrResultCommand), typeof(OcrResult));
                await _ocrResultWriteRepository.Update(ocrResultToUpdate);
                return _mapper.Map<BaseOcrResultDto>(ocrResultToUpdate);
            }
        }
    }
}
