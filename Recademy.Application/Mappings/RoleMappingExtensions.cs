using Recademy.Core.Models.Roles;
using Recademy.Dto.Roles;

namespace Recademy.Application.Mappings;

public static class RoleMappingExtensions
{
    public static UserRoleDto ToDto(this IUserRole role)
    {
        if (role is null)
            return null;

        return new UserRoleDto
        {
            RoleId = role.Id,
            Name = role.Name,
            RequiredPoints = role.RequiredPoints,
        };
    }
}