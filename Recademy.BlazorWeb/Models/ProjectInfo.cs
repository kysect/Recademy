using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.BlazorWeb.Models
{
    public class ProjectInfo
    {
        [Key] 
        public int Id { get; set; }

        public string Title { get; set; }

        [ForeignKey("User")] 
        public int AuthorId { get; set; }

        public string Description { get; set; }

        public string GithubLink { get; set; }


        public User User { get; set; }
        public ICollection<ProjectSkill> Skills { get; set; }
    }
}