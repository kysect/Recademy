using Recademy.Core.Models.Users;
using Recademy.Core.Types;

namespace Recademy.DataAccess.Repositories.Abstractions;

public interface IUserRepository
{
    RecademyUser Find(int id);
    RecademyUser FindRecademyUser(string username);
    User FindUser(string username);
    RecademyUser GetUserById(int id);
    User UpdateUserRole(User user, UserType userType);
}