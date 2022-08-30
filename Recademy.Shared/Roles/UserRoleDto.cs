namespace Recademy.Dto.Roles;

public sealed class UserRoleDto
{
    public UserRoleDto()
    {
    }

    public string Name { get; init; }
    public int RequiredPoints { get; init; }
}