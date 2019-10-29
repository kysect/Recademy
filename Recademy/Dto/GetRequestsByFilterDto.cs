using System.Collections.Generic;

namespace Recademy.Dto
{
    public class GetRequestsByFilterDto
    {
        public int UserId { get; set; }
        public List<string> Tags { get; set; }

        public static GetRequestsByFilterDto Of(int userId, string tag)
        {
            return new GetRequestsByFilterDto
            {
                UserId = userId,
                Tags = new List<string> { tag }
            };
        }
        public static GetRequestsByFilterDto Of(int userId, List<string> tag)
        {
            return new GetRequestsByFilterDto
            {
                UserId = userId,
                Tags = tag
            };
        }
    }
}
