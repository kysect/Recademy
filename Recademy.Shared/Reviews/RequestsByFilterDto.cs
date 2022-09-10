using System.Collections.Generic;

namespace Recademy.Dto.Reviews;

public class ReviewRequestSearchContextDto
{
    public int UserId { get; init; }
    public string ProjectName { get; init; }
    public int? AuthorId { get; init; }
    public ICollection<string> Tags { get; init; }
    public bool WithoutReviewed { get; init; }
}