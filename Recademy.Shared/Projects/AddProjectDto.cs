using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Dto.Projects;

public class AddProjectDto
{
    public AddProjectDto()
    {

    }

    public AddProjectDto(string projectName, int userId, string url)
    {
        UserId = userId;
        ProjectUrl = url;
        ProjectName = projectName;
        Tags = new List<string>();
    }

    [Required]
    public int UserId { get; init; }
    [Required]
    public string ProjectName { get; init; }
    [Required]
    public string ProjectUrl { get; init; }
    public ICollection<string> Tags { get; init; }
}