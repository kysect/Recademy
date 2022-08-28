using System.ComponentModel.DataAnnotations;

namespace Recademy.Shared.Dtos
{
    public class ReviewRequestAddDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}