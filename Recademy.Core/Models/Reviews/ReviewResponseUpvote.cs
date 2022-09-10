using Recademy.Core.Models.Users;

using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Reviews;

public class ReviewResponseUpvote
{
    [ForeignKey(nameof(ReviewResponse))]
    public int ReviewResponseId { get; set; }
    public ReviewResponse ReviewResponse { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }
}