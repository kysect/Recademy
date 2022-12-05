namespace Recademy.Dto.Reviews;

public class CreateReviewRequestDto
{
    public int UserId { get; init; }
    public int ProjectId { get; init; }
    public string Comment { get; init; }
}