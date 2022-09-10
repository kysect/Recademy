using Recademy.Core.Types;

using System.ComponentModel.DataAnnotations;

namespace Recademy.Core.Models.Users;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string GithubUsername { get; set; }
    public UserType UserType { get; set; }
}