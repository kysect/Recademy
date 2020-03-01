namespace Recademy.Library.Dto
{
    public class SendReviewResponseDto
    {
        public SendReviewResponseDto(int id, string text)
        {
            Id = id;
            ReviewText = text;
        }

        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int UserId { get; set; }
    }
}
