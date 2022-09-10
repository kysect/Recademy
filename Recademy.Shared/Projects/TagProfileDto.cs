using System.Collections.Generic;

namespace Recademy.Dto.Projects;

public class TagProfileDto
{
    public string TagName { get; init; }
    public IReadOnlyCollection<ProjectInfoDto> Projects { get; init; }
}