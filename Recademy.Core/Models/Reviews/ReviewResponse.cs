using Recademy.Core.Models.Users;
using Recademy.Core.Types;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Reviews;

public class ReviewResponse
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreationTime { get; set; }
    public ReviewConclusion ReviewConclusion { get; set; }

    [ForeignKey("ReviewRequest")]
    public int ReviewRequestId { get; set; }
    public ReviewRequest ReviewRequest { get; set; }

    [ForeignKey("User")]
    public int ReviewerId { get; set; }
    public RecademyUser Reviewer { get; set; }
}