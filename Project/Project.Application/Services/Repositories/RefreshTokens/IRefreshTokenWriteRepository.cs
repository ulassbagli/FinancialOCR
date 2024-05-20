using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories.RefreshTokens;

public interface IRefreshTokenWriteRepository: IWriteRepository<RefreshToken>
{
    
}