using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Roles;
using Recademy.DataAccess;
using Recademy.Dto.Roles;

namespace Recademy.Application.Services.Implementations;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IReadOnlyCollection<IUserRole> _roles = new List<IUserRole>
    {
        new EvangelistUserRole(),
    };

    private readonly RecademyContext _context;

    public UserRoleService(RecademyContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<UserRoleDto> GetAllRoles()
    {
        return _roles
            .Select(role => role.ToDto())
            .ToList();
    }

    public UserRoleDto GetUserRole(int userId)
    {
        UserRoleAssociation roleAssociation = _context.UserRoleAssociations
            .SingleOrDefault(association => association.UserId == userId);

        if (roleAssociation is null)
            return null;

        return _roles
            .First(role => role.Id.Equals(roleAssociation.RoleId))
            .ToDto();
    }

    public async Task ChangeUserRole(int userId, int roleId)
    {
        UserRoleAssociation roleAssociation = _context.UserRoleAssociations
            .SingleOrDefault(association => association.UserId == userId);

        if (roleAssociation is not null)
        {
            roleAssociation.RoleId = roleId;
            _context.UserRoleAssociations.Update(roleAssociation);
        }
        else
        {
            _context.Add(new UserRoleAssociation(userId, roleId));
        }

        await _context.SaveChangesAsync();
    }
}