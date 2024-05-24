using AutoMapper;
using MediatR;
using Project.Application.Features.OcrResults.Dtos.BaseDto;
using Project.Application.Features.OcrResults.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Queries
{
    public class GetByIdOcrResultQuery : IRequest<BaseOcrResultDto>
    {
        public string Id { get; set; }

        public class GetByIdOcrResultQueryHandler : IRequestHandler<GetByIdOcrResultQuery, BaseOcrResultDto>
        {
            private readonly OcrResultBusinessRules _ocrResultBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdOcrResultQueryHandler(IMapper mapper, OcrResultBusinessRules ocrResultBusinessRules)
            {
                _ocrResultBusinessRules = ocrResultBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseOcrResultDto> Handle(GetByIdOcrResultQuery request, CancellationToken cancellationToken)
            {
                var ocrResult = await _ocrResultBusinessRules.CheckIfOcrResultDoesNotExistsAndGetOcrResult(request.Id);

                return _mapper.Map<BaseOcrResultDto>(ocrResult);
            }
        }
    }
}
