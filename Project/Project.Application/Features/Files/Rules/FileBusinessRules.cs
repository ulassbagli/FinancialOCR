using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.Files.Constants;
using Project.Application.Services.Repositories.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Project.Domain.Entities.File;


namespace Project.Application.Features.Files.Rules
{
    public class FileBusinessRules
    {
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public FileBusinessRules(IFileReadRepository fileReadRepository, IUserReadRepository userReadRepository)
        {
            _fileReadRepository = fileReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfFileAlreadyExists(string userId)
        {
            var file = await _fileReadRepository.GetAsync(b => b.UserId == Guid.Parse(userId));
            if (file is not null) throw new BusinessException(FileMessages.FileAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(FileMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfFileDoesNotExists(File file)
        {
            if (file == null) throw new BusinessException(FileMessages.FileNotFound); //TODO: Localize message.
        }
        public async Task<File> CheckIfFileDoesNotExistsAndGetFile(string fileId)
        {
            var file = await _fileReadRepository.GetByIdAsync(fileId);
            if (file == null) throw new BusinessException(FileMessages.FileNotFound); //TODO: Localize message.
            return file;
        }
    }
