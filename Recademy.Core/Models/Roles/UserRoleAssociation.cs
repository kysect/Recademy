using System;
using Recademy.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Roles;

public sealed class UserRoleAssociation
{
    [ForeignKey("RecademyUser")]
    public int UserId { get; set; }
    public RecademyUser User { get; set; }
    public int RoleId { get; set; }

    public UserRoleAssociation(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}