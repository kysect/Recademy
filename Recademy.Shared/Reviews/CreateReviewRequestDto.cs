using System.ComponentModel.DataAnnotations;

namespace Recademy.Dto.Reviews;

public class CreateReviewRequestDto
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public string Comment { get; set; }
}