using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.OcrResults.Models;
using Project.Application.Services.Repositories.OcrResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Queries
{
    public class GetListOcrResultByDynamicQuery : IRequest<OcrResultListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.OcrResult.ToString() };

        public class GetListOcrResultByDynamicQueryHandler : IRequestHandler<GetListOcrResultByDynamicQuery, OcrResultListModel>
        {
            private readonly IOcrResultReadRepository _ocrResultReadRepository;
            private readonly IMapper _mapper;

            public GetListOcrResultByDynamicQueryHandler(IMapper mapper, IOcrResultReadRepository ocrResultReadRepository)
            {
                _ocrResultReadRepository = ocrResultReadRepository;
                _mapper = mapper;
            }

            public async Task<OcrResultListModel> Handle(GetListOcrResultByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _ocrResultReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.resultText),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<OcrResultListModel>(models);
            }
        }
    }
}
