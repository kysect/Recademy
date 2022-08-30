using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Dto.Roles;

namespace Recademy.Application.Services.Abstractions;

public interface IUserRoleService
{
    IReadOnlyCollection<UserRoleDto> GetAllRoles();
    UserRoleDto GetUserRole(int userId);
    Task ChangeUserRole(int userId, int roleId);
}