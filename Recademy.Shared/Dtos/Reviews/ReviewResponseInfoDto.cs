using System;
using Recademy.Core.Types;

namespace Recademy.Shared.Dtos.Reviews
{
    public class ReviewResponseInfoDto
    {
        public ReviewResponseInfoDto()
        {

        }

        public ReviewResponseInfoDto(
            int id, 
            string description, 
            DateTime creationTime, 
            ReviewConclusion reviewConclusion, 
            ReviewRequestInfoDto reviewRequest, 
            int reviewerId)
        {
            Id = id;
            Description = description;
            CreationTime = creationTime;
            ReviewConclusion = reviewConclusion;
            ReviewRequest = reviewRequest;
            ReviewerId = reviewerId;
        }

        public int Id { get; init; }
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public ReviewConclusion ReviewConclusion { get; init; }

        public ReviewRequestInfoDto ReviewRequest { get; init; }
        public int ReviewerId { get; init; }
    }
}