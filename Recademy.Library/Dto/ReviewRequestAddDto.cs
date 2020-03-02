namespace Recademy.Library.Dto
{
    public class ReviewRequestAddDto
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
    }
}