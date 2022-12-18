using Recademy.Core.Models.Roles;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Providers;

public static class UserRoleProvider
{
    public static readonly IReadOnlyCollection<IUserRole> Roles = new List<IUserRole>
    {
        new EvangelistUserRole(),
    };

    public static IUserRole FindRoleById(int roleId)
    {
        return Roles.FirstOrDefault(role => role.Id == roleId);
    }
}