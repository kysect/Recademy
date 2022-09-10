using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Reviews;

public class ReviewRequest
{
    [Key]
    public int Id { get; init; }
    public DateTime DateCreate { get; init; }
    public ProjectState State { get; set; }
    public string Description { get; init; }
    public ICollection<ReviewResponse> ReviewResponse { get; init; }

    [ForeignKey("ProjectInfo")]
    public int ProjectId { get; init; }
    public ProjectInfo ProjectInfo { get; init; }

    [ForeignKey("User")]
    public int UserId { get; init; }
    public RecademyUser User { get; init; }
}