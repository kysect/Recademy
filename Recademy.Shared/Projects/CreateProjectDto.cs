using System.Collections.Generic;

namespace Recademy.Dto.Projects;

public record CreateProjectDto(int AuthorId, string Title, string Description, string Link, ICollection<string> Tags);