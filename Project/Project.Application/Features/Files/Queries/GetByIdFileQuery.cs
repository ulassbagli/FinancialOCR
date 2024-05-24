using AutoMapper;
using MediatR;
using Project.Application.Features.Files.Dtos.BaseDto;
using Project.Application.Features.Files.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Features.Files.Queries
{
    public class GetByIdFileQuery : IRequest<BaseFileDto>
    {
        public string Id { get; set; }

        public class GetByIdFileQueryHandler : IRequestHandler<GetByIdFileQuery, BaseFileDto>
        {
            private readonly FileBusinessRules _fileBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdFileQueryHandler(IMapper mapper, FileBusinessRules fileBusinessRules)
            {
                _fileBusinessRules = fileBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseFileDto> Handle(GetByIdFileQuery request, CancellationToken cancellationToken)
            {
                var file = await _fileBusinessRules.CheckIfFileDoesNotExistsAndGetFile(request.Id);

                return _mapper.Map<BaseFileDto>(file);
            }
        }
    }
}
