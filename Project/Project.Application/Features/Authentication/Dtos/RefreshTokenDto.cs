namespace Application.Features.Authentication.Dtos;

public class RefreshTokenDto
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}