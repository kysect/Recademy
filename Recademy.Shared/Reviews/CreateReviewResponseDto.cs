using Recademy.Dto.Enums;

namespace Recademy.Dto.Reviews;

public class CreateReviewResponseDto
{
    public CreateReviewResponseDto()
    {
    }

    public int RequestId { get; init; }
    public int ReviewerId { get; init; }
    public ReviewConclusionDto ReviewConclusion { get; init; }
    public string Comment { get; init; }
}