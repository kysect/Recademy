using System;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Models;

namespace Recademy.Services
{
    public class ReviewService
    {
        public RecademyContext Context;

        public ReviewService(RecademyContext context)
        {
            Context = context;
        }

        public ReviewRequest AddReviewRequest(AddReviewRequestDto argues)
        {
            ReviewRequest newRequest = new ReviewRequest()
            {
                DateCreate = DateTime.Now,
                ProjectId = argues.ProjectId
            };
            Context.Add(newRequest);
            Context.SaveChanges();

            return newRequest;
        }

        public ReviewResponse AcceptReviewRequest(AcceptReviewRequestDto argues)
        {
            ReviewResponse newReview = new ReviewResponse()
            {
                ReviewRequestId = argues.ReviewRequestId,
                Description = argues.ReviewText
            };
            Context.ReviewResponses.Add(newReview);
            return newReview;
        }
    }
}