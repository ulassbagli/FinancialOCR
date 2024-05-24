using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Files.Models;
using Project.Application.Services.Repositories.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Files.Queries
{
    public class GetListFileByDynamicQuery : IRequest<FileListModel>, ISecuredRequest
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = { OperationClaimsEnum.File.ToString() };

        public class GetListFileByDynamicQueryHandler : IRequestHandler<GetListFileByDynamicQuery, FileListModel>
        {
            private readonly IFileReadRepository _fileReadRepository;
            private readonly IMapper _mapper;

            public GetListFileByDynamicQueryHandler(IMapper mapper, IFileReadRepository fileReadRepository)
            {
                _fileReadRepository = fileReadRepository;
                _mapper = mapper;
            }

            public async Task<FileListModel> Handle(GetListFileByDynamicQuery request, CancellationToken cancellationToken)
            {
                var models = await _fileReadRepository.GetListByDynamicAsync(request.Dynamic, include:
                    m => m.Include(c => c.Name),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);
                return _mapper.Map<FileListModel>(models);
            }
        }
    }
}
