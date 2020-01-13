namespace Recademy.Library.Dto
{
    public class SendReviewRequestDto
    {
        public SendReviewRequestDto(int id, string text)
        {
            ReviewRequestId = id;
            ReviewText = text;
        }
        public int ReviewRequestId { get; set; }
        public string ReviewText { get; set; }
    }
}
