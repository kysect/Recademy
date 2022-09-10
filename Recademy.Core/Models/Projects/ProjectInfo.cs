using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Skills;
using Recademy.Core.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Projects;

public class ProjectInfo
{
    [Key]
    public int Id { get; init; }
    public string Title { get; init; }

    [ForeignKey("User")]
    public int AuthorId { get; init; }
    public virtual User User { get; init; }

    public string Description { get; init; }

    public string GithubLink { get; init; }

    public virtual ICollection<ProjectSkill> Skills { get; init; }
    public virtual ICollection<ReviewRequest> ReviewRequests { get; init; }
}