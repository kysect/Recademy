using System;
using Recademy.Models;

namespace Mock.Generators
{
    public class ReviewResponseGenerator
    {
        private readonly Random _random = new Random();

        private readonly string _title = "Some Description#";

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }


        /// <summary>
        ///     get a random name
        /// </summary>
        /// <returns></returns>
        public ReviewResponse GetResponse(ReviewRequest reviewRequest, int reviewerId)
        {
            int randVal = _random.Next();
            ReviewResponse result = new ReviewResponse
            {
                ReviewRequest = reviewRequest,
                Description = $"{_title}{randVal}",
                ReviewRequestId = reviewRequest.Id,
                ReviewerId = reviewerId,
                CreationTime = RandomDay()
            };
            return result;
        }
    }
}