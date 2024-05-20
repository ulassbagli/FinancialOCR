using System.Security.Claims;
using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}