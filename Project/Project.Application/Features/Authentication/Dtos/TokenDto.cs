using Core.Security.JWT;

namespace Application.Features.Authentication.Dtos;

public class TokenDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshTokenDto RefreshToken { get; set; }
}