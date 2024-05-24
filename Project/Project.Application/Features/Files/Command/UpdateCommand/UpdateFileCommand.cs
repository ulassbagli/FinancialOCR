using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Files.Dtos.BaseDto;
using Project.Application.Features.Files.Rules;
using Project.Application.Services.Repositories.Files;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;

namespace Project.Application.Features.Files.Command.UpdateCommand
{
    public class UpdateFileCommand : IRequest<BaseFileDto>
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, BaseFileDto>
        {
            private readonly IFileWriteRepository _fileWriteRepository;
            private readonly FileBusinessRules _fileBusinessRules;
            private readonly IMapper _mapper;

            public UpdateFileCommandHandler(IMapper mapper, IFileWriteRepository fileWriteRepository, FileBusinessRules fileBusinessRules)
            {
                _fileWriteRepository = fileWriteRepository;
                _fileBusinessRules = fileBusinessRules;
                _mapper = mapper;
            }

            public async Task<BaseFileDto> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
            {
                var fileToUpdate = await _fileBusinessRules.CheckIfFileDoesNotExistsAndGetFile(request.Id);
                await _fileBusinessRules.CheckIfUserDoesNotExistsAndGetUser(request.UserId);

                _mapper.Map(request, fileToUpdate, typeof(UpdateFileCommand), typeof(File));
                await _fileWriteRepository.Update(fileToUpdate);
                return _mapper.Map<BaseFileDto>(fileToUpdate);
            }
        }
    }
}
