using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Models;
using Recademy.Services.Abstraction;
using Recademy.Types;

namespace Recademy.Services
{
    public class ReviewService : IReviewService
    {
        public RecademyContext Context;

        public ReviewService(RecademyContext context)
        {
            Context = context;
        }

        public List<ReviewRequest> GetReviewRequests()
        {
            List<ReviewRequest> reqList = Context.ReviewRequests.Where(s => s.State == ProjectState.Requested).ToList();

            return reqList;
        }

        public ReviewRequest AddReviewRequest(int ProjectId)
        {
            ReviewRequest newRequest = new ReviewRequest()
            {
                DateCreate = DateTime.Now,
                ProjectId = ProjectId,
                State = ProjectState.Requested
            };
            Context.Add(newRequest);
            Context.SaveChanges();

            return newRequest;
        }

        public ReviewResponse SendReviewResponse(SendReviewRequestDto argues)
        {
            ReviewResponse newReview = new ReviewResponse()
            {
                ReviewRequestId = argues.ReviewRequestId,
                Description = argues.ReviewText
            };
            Context.ReviewRequests.Find(argues.ReviewRequestId).State = ProjectState.Reviewed;
            Context.ReviewResponses.Add(newReview);
            Context.SaveChanges();
            return newReview;
        }
    }
}