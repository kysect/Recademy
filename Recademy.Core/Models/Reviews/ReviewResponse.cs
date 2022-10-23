using Recademy.Core.Models.Users;
using Recademy.Core.Types;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Reviews;

public class ReviewResponse
{
    [Key]
    public int Id { get; init; }
    [ForeignKey("Request")]
    public int RequestId { get; init; }
    public virtual ReviewRequest Request { get; set; }

    [ForeignKey("User")]
    public int ReviewerId { get; init; }
    public virtual RecademyUser Reviewer { get; init; }
    public ReviewConclusion ReviewConclusion { get; init; }
    public string Description { get; init; }
    public DateTime CreationTime { get; init; }
}