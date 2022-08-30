namespace Recademy.Core.Models.Roles;

public interface IUserRole
{
    public int Id { get; }
    public string Name { get; }
    public int RequiredPoints { get; }
}