using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Repositories
{
    public interface IUserRepository
    {
        User Find(int id);
        User FindByUsername(string username);
        User Get(int id);
        User UpdateUserRole(User user, UserType userType);
    }
}