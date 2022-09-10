using Recademy.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Roles;

public class UserRoleAssociation
{
    [ForeignKey("RecademyUser")]
    public int UserId { get; init; }
    public virtual RecademyUser User { get; init; }
    public int RoleId { get; set; }

    public UserRoleAssociation(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}