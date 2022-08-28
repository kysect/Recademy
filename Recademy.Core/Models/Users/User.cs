using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Recademy.Core.Models.Achievements;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Skills;
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