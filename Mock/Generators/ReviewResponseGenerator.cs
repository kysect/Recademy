using System;
using System.Collections.Generic;
using System.Text;
using Recademy.Models;

namespace Mock.Generators
{
    class ReviewResponseGenerator
    {
        private readonly string _title = "Some Description#";

        private readonly Random _random = new Random();

        /// <summary>
        /// get a random name
        /// </summary>
        /// <returns></returns>
        public ReviewResponse GetResponse(ReviewRequest reviewRequest, int reviewerId)
        {
            var randVal = _random.Next();
            ReviewResponse result = new ReviewResponse { ReviewRequest = reviewRequest, Description = $"{_title}{randVal}", ReviewRequestId = reviewRequest.Id, ReviewerId = reviewerId};
            return result;
        }
    }
}
