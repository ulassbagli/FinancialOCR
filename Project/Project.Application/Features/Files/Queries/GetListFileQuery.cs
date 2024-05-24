using AutoMapper;
using Core.Application.Requests;
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
    public class GetListFileQuery : IRequest<FileListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListFileQueryHandler : IRequestHandler<GetListFileQuery, FileListModel>
        {
            private readonly IMapper _mapper;
            private readonly IFileReadRepository _fileReadRepository;

            public GetListFileQueryHandler(IFileReadRepository fileReadRepository, IMapper mapper)
            {
                _mapper = mapper;
                _fileReadRepository = fileReadRepository;
            }

            public async Task<FileListModel> Handle(GetListFileQuery request, CancellationToken cancellationToken)
            {
                var files = await _fileReadRepository.GetListAsync(include: m => m.Include(p => p.Name),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                return _mapper.Map<FileListModel>(files);
            }
        }
    }
}
