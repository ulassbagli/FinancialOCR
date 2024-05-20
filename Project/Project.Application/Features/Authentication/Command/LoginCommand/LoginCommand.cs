using Application.Features.Authentication.Dtos;
using Application.Features.Authentication.Rules;
using Application.Services.Repositories.RefreshTokens;
using Application.Services.Repositories.Users;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Command.LoginCommand;

public class LoginCommand: UserForLoginDto, IRequest<TokenDto>
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenDto>
    {
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationBusinessRules _authenticationBusinessRules;

        public LoginCommandHandler(IUserReadRepository userReadRepository, IMapper mapper, ITokenHelper tokenHelper, 
            AuthenticationBusinessRules authenticationBusinessRules, IHttpContextAccessor httpContextAccessor, IRefreshTokenWriteRepository refreshTokenWriteRepository)
        {
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _userReadRepository = userReadRepository;
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenWriteRepository = refreshTokenWriteRepository;
            _authenticationBusinessRules = authenticationBusinessRules;
        }

        public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(
                predicate: u => u.Email.Equals(request.Email),
                include: m => m.Include(u => u.UserOperationClaims).ThenInclude(uo => uo.OperationClaim));
            
            await _authenticationBusinessRules.CheckIfUserDoesNotExists(user);
            await _authenticationBusinessRules.CheckIfPasswordCorrect(request.Password, user.PasswordHash, user.PasswordSalt);
            
            var clientIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var operationClaims = user.UserOperationClaims.Select(uo => uo.OperationClaim).ToList();
            var refreshToken = _tokenHelper.CreateRefreshToken(user, clientIpAddress);
            await _refreshTokenWriteRepository.AddAsync(refreshToken);
            
            return new TokenDto
            {
                AccessToken = _tokenHelper.CreateToken(user, operationClaims),
                RefreshToken = _mapper.Map<RefreshToken, RefreshTokenDto>(refreshToken)
            };
        }
    }
}