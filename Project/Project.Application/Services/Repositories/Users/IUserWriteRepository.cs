using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories.Users;

public interface IUserWriteRepository: IWriteRepository<User>
{
}