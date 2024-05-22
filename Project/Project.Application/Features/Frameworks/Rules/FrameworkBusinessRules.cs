using Application.Features.Frameworks.Constants;
using Application.Features.ProgrammingLanguages.Constants;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Project.Application.Services.Repositories.Frameworks;

namespace Application.Features.Frameworks.Rules;

public class FrameworkBusinessRules
{
    private readonly IFrameworkReadRepository _frameworkReadRepository;

    public FrameworkBusinessRules(IFrameworkReadRepository frameworkReadRepository)
    {
        _frameworkReadRepository = frameworkReadRepository;
    }
    
    public async Task CheckIfFrameworkNameIsAlreadyExists(string name)
    {
        IPaginate<Framework> result = await _frameworkReadRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException(FrameworkMessages.FrameworkNameAlreadyExists); //TODO: Localize message.
    }
    public async Task CheckIfFrameworkDoesNotExists(Framework framework)
    {
        if (framework == null) throw new BusinessException(FrameworkMessages.FrameworkNotFound); //TODO: Localize message.
    }
}