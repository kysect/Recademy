using System.ComponentModel.DataAnnotations;

namespace Recademy.Library.Dto
{
    public class SendReviewResponseDto
    {
        public SendReviewResponseDto()
        {
            
        }

        public SendReviewResponseDto(string text, int userId)
        {
            ReviewText = text;
            UserId = userId;
        }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
