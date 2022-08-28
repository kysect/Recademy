using Recademy.Dto.Enums;

using System;

namespace Recademy.Dto.Reviews
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
            ReviewConclusionDto reviewConclusion,
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
        public ReviewConclusionDto ReviewConclusion { get; init; }

        public ReviewRequestInfoDto ReviewRequest { get; init; }
        public int ReviewerId { get; init; }
    }
}