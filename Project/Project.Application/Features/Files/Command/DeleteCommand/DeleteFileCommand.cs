using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Files.Dtos;
using Project.Application.Features.Files.Rules;
using Project.Application.Services.Repositories.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Features.Files.Command.DeleteCommand
{
    public class DeleteFileCommand : IRequest<DeletedFileDto>
    {
        public string Id { get; set; }
        public bool isSoftDelete { get; set; }

        public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, DeletedFileDto>
        {
            private readonly IFileWriteRepository _fileWriteRepository;
            private readonly IMapper _mapper;
            private readonly FileBusinessRules _fileBusinessRules;

            public DeleteFileCommandHandler(IFileWriteRepository fileWriteRepository, IMapper mapper,
                FileBusinessRules fileBusinessRules)
            {
                _mapper = mapper;
                _fileBusinessRules = fileBusinessRules;
                _fileWriteRepository = fileWriteRepository;
            }

            public async Task<DeletedFileDto> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
            {
                var file = await _fileBusinessRules.CheckIfFileDoesNotExistsAndGetFile(request.Id);

                File deletedFile;
                if (request.isSoftDelete)
                    deletedFile = await _fileWriteRepository.SoftRemove(file);
                else
                    deletedFile = await _fileWriteRepository.HardRemove(file);

                return _mapper.Map<DeletedFileDto>(deletedFile);
            }
        }
    }
}
