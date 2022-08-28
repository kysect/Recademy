using Recademy.Core.Models.Users;
using Recademy.Core.Types;

namespace Recademy.DataAccess.Repositories.Abstractions
{
    public interface IUserRepository
    {
        RecademyUser Find(int id);
        RecademyUser FindByUsername(string username);
        RecademyUser Get(int id);
        User UpdateUserRole(User user, UserType userType);
    }
}