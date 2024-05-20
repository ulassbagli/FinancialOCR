using Application.Features.Developers.Constants;
using Application.Services.Repositories.Developers;
using Application.Services.Repositories.Users;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Features.Developers.Rules;

public class DeveloperBusinessRules
{
    private readonly IDeveloperReadRepository _developerReadRepository;
    private readonly IUserReadRepository _userReadRepository;

    public DeveloperBusinessRules(IDeveloperReadRepository developerReadRepository, IUserReadRepository userReadRepository)
    {
        _developerReadRepository = developerReadRepository;
        _userReadRepository = userReadRepository;
    }
    public async Task CheckIfDeveloperAlreadyExists(string userId)
    {
        var developer = await _developerReadRepository.GetAsync(b => b.UserId == Guid.Parse(userId));
        if (developer is not null) throw new BusinessException(DeveloperMessages.DeveloperAlreadyExists); //TODO: Localize message.
    }
    public async Task<User> CheckIfUserDoesNotExistsAndGetUser(string userId)
    {
        var user = await _userReadRepository.GetByIdAsync(userId);
        if (user is null) throw new BusinessException(DeveloperMessages.UserNotFound); //TODO: Localize message.
        return user;
    }
    public async Task CheckIfDeveloperDoesNotExists(Developer developer)
    {
        if (developer == null) throw new BusinessException(DeveloperMessages.DeveloperNotFound); //TODO: Localize message.
    }
    public async Task<Developer> CheckIfDeveloperDoesNotExistsAndGetDeveloper(string developerId)
    {
        var developer = await _developerReadRepository.GetByIdAsync(developerId);
        if (developer == null) throw new BusinessException(DeveloperMessages.DeveloperNotFound); //TODO: Localize message.
        return developer;
    }
}