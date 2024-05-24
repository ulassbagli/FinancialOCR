using AutoMapper;
using Core.Application.Requests;
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
    public class GetListOcrResultQuery : IRequest<OcrResultListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListOcrResultQueryHandler : IRequestHandler<GetListOcrResultQuery, OcrResultListModel>
        {
            private readonly IMapper _mapper;
            private readonly IOcrResultReadRepository _ocrResultReadRepository;

            public GetListOcrResultQueryHandler(IOcrResultReadRepository ocrResultReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _ocrResultReadRepository = ocrResultReadRepository;
            }

            public async Task<OcrResultListModel> Handle(GetListOcrResultQuery request, CancellationToken cancellationToken)
            {
                var ocrResults = await _ocrResultReadRepository.GetListAsync(include: m => m.Include(p => p.resultText),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<OcrResultListModel>(ocrResults);
            }
        }
    }
}
