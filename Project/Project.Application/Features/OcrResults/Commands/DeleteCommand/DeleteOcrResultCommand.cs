using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.OcrResults.Dtos;
using Project.Application.Features.OcrResults.Dtos.BaseDto;
using Project.Application.Features.OcrResults.Rules;
using Project.Application.Services.Repositories.OcrResults;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Commands.DeleteCommand
{
    public class DeleteOcrResultCommand : IRequest<DeletedOcrResultDto>
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteOcrResultCommandHandler : IRequestHandler<DeleteOcrResultCommand, DeletedOcrResultDto>
        {
            private readonly IOcrResultWriteRepository _ocrResultWriteRepository;
            private readonly IMapper _mapper;
            private readonly OcrResultBusinessRules _ocrResultBusinessRules;

            public DeleteOcrResultCommandHandler(IOcrResultWriteRepository ocrResultWriteRepository, IMapper mapper,
                OcrResultBusinessRules ocrResultBusinessRules)
            {
                _mapper = mapper;
                _ocrResultBusinessRules = ocrResultBusinessRules;
                _ocrResultWriteRepository = ocrResultWriteRepository;
            }

            public async Task<DeletedOcrResultDto> Handle(DeleteOcrResultCommand request, CancellationToken cancellationToken)
            {
                var ocrResult = await _ocrResultBusinessRules.CheckIfOcrResultDoesNotExistsAndGetOcrResult(request.Id);

                OcrResult deletedOcrResult;
                if (request.isSoftDelete)
                    deletedOcrResult = await _ocrResultWriteRepository.SoftRemove(ocrResult);
                else
                    deletedOcrResult = await _ocrResultWriteRepository.HardRemove(ocrResult);

                return _mapper.Map<DeletedOcrResultDto>(deletedOcrResult);
            }
        }
    }
}
