using System.ComponentModel.DataAnnotations;
using Recademy.Core.Types;

namespace Recademy.Core.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string GithubUsername { get; set; }
        public UserType UserType { get; set; }
    }
}