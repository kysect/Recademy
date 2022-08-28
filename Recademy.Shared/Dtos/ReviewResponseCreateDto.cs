using System.ComponentModel.DataAnnotations;
using Recademy.Core.Types;

namespace Recademy.Shared.Dtos
{
    public class ReviewResponseCreateDto
    {
        public ReviewResponseCreateDto()
        {
            
        }

        public ReviewResponseCreateDto(string text, int userId, int reviewRequestId, ReviewConclusion reviewConclusion)
        {
            ReviewText = text;
            UserId = userId;
            ReviewRequestId = reviewRequestId;
            ReviewConclusion = reviewConclusion;
        }

        [Required]
        public string ReviewText { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ReviewRequestId { get; set; }
        [Required]
        public ReviewConclusion ReviewConclusion { get; set; }
    }
}
