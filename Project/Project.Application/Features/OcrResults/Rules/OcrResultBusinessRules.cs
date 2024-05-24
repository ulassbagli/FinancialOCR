using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Project.Application.Features.OcrResults.Constants;
using Project.Application.Services.Repositories.OcrResults;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.OcrResults.Rules
{
    public class OcrResultBusinessRules
    {
        private readonly IOcrResultReadRepository _ocrResultReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public OcrResultBusinessRules(IOcrResultReadRepository ocrResultReadRepository, IUserReadRepository userReadRepository)
        {
            _ocrResultReadRepository = ocrResultReadRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task CheckIfOcrResultAlreadyExists(string userId)
        {
            var ocrResult = await _ocrResultReadRepository.GetAsync(b => b.UserId == Guid.Parse(userId));
            if (ocrResult is not null) throw new BusinessException(OcrResultMessages.OcrResultAlreadyExists); //TODO: Localize message.
        }
        public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
        {
            var user = await _userReadRepository.GetByIdAsync(userId);
            if (user is null) throw new BusinessException(OcrResultMessages.UserNotFound); //TODO: Localize message.
            return user;
        }
        public async Task CheckIfOcrResultDoesNotExists(OcrResult ocrResult)
        {
            if (ocrResult == null) throw new BusinessException(OcrResultMessages.OcrResultNotFound); //TODO: Localize message.
        }
        public async Task<OcrResult> CheckIfOcrResultDoesNotExistsAndGetOcrResult(string ocrResultId)
        {
            var ocrResult = await _ocrResultReadRepository.GetByIdAsync(ocrResultId);
            if (ocrResult == null) throw new BusinessException(OcrResultMessages.OcrResultNotFound); //TODO: Localize message.
            return ocrResult;
        }
    }
}
