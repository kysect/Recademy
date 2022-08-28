using Recademy.Core.Models;
using Recademy.Core.Types;

namespace Recademy.DataAccess.Repositories
{
    public interface IUserRepository
    {
        User Find(int id);
        User FindByUsername(string username);
        User Get(int id);
        User UpdateUserRole(User user, UserType userType);
    }
}