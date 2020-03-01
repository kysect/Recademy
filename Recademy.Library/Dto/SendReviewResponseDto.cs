namespace Recademy.Library.Dto
{
    public class SendReviewResponseDto
    {
        public SendReviewResponseDto(string text, int userId)
        {
            ReviewText = text;
            UserId = userId;
        }

        public string ReviewText { get; set; }
        public int UserId { get; set; }
    }
}
