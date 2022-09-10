using Recademy.Dto.Enums;

using System.ComponentModel.DataAnnotations;

namespace Recademy.Dto.Reviews;

public class ReviewResponseCreateDto
{
    public ReviewResponseCreateDto()
    {
    }

    [Required]
    public string ReviewText { get; init; }
    [Required]
    public int UserId { get; init; }
    [Required]
    public int ReviewRequestId { get; init; }
    [Required]
    public ReviewConclusionDto ReviewConclusion { get; init; }
}