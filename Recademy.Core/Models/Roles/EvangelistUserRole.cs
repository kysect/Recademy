namespace Recademy.Core.Models.Roles;

public sealed class EvangelistUserRole : IUserRole
{
    public int Id => 1;
    public string Name => "Евангелист";
    public int RequiredPoints => 1000;
}