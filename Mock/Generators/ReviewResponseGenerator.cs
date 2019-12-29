using System;
using Recademy.Models;

namespace Mock.Generators
{
    public class ReviewResponseGenerator
    {
        private readonly Random _random = new Random();

        private const string Title = "Some Description#";

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
            return new ReviewResponse
            {
                ReviewRequest = reviewRequest,
                Description = $"{Title}{randVal}",
                ReviewRequestId = reviewRequest.Id,
                ReviewerId = reviewerId,
                CreationTime = RandomDay()
            };
        }
    }
}