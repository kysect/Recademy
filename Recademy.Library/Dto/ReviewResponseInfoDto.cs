using System;
using Recademy.Library.Models;
using Recademy.Library.Tools;
using Recademy.Library.Types;

namespace Recademy.Library.Dto
{
    public class ReviewResponseInfoDto
    {
        public ReviewResponseInfoDto()
        {
            
        }

        public ReviewResponseInfoDto(ReviewResponse model)
        {
            Id = model.Id;
            Description = model.Description;
            CreationTime = model.CreationTime;
            ReviewConclusion = model.ReviewConclusion;
            ReviewRequest = model.ReviewRequest.Maybe(r => new ReviewRequestInfoDto(r));
            ReviewerId = model.ReviewerId;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public ReviewConclusion ReviewConclusion { get; set; }

        public ReviewRequestInfoDto ReviewRequest { get; set; }
        public int ReviewerId { get; set; }
    }
}