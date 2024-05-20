using Core.CrossCuttingConcerns.Constant;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
        
        if(roleClaims is null) throw new AuthorizationException("Claims not found"); //TODO: Add localization and use resource file
        
        bool isNotMatchedARoleClaimWithRequestRoles = roleClaims.Any(roleClaim => !request.Roles.Contains(roleClaim));
        if(isNotMatchedARoleClaimWithRequestRoles) throw new AuthorizationException(AspectMessages.AuthorizationDenied); //TODO: Add localization

        var response = await next();
        return response;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
