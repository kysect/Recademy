using Recademy.Dto.Enums;
using Recademy.Dto.Users;

namespace Recademy.Common;
public static class UserPermissionExtensions
{
    public static bool HasMentorRights(this UserInfoDto userDto)
    {
        return userDto.UserType is UserTypeDto.Admin or UserTypeDto.Mentor;
    }
}