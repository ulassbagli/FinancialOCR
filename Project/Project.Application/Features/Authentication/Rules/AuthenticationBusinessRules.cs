using System.Security.Authentication;
using Application.Features.Authentication.Constants;
using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Authentication.Rules;

public class AuthenticationBusinessRules
{
    private readonly IUserReadRepository _userReadRepository;

    public AuthenticationBusinessRules(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }
    
    public async Task CheckIfUserAlreadyExists(string userId)
    {
        var user = await _userReadRepository.GetByIdAsync(userId);
        if (user is not null) throw new BusinessException(AuthenticationMessages.UserAlreadyExist); //TODO: Localize message.
    }
    public async Task CheckIfUserDoesNotExists(User user)
    {
        if (user is null) throw new BusinessException(AuthenticationMessages.UserNotFound); //TODO: Localize message.
    }
    public async Task CheckIfPasswordCorrect(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt) is false)
            throw new BusinessException(AuthenticationMessages.UserPasswordDoesNotCorrect); //TODO: Localize message.
    }

    public async Task CheckIfRefresfTokenIsValid(RefreshToken refreshToken)
    {
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
            throw new AuthenticationException(AuthenticationMessages.InvalidRefreshToken);
    }
}