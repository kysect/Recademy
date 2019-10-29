using Recademy.Models;
using System;

namespace Recademy.Dto
{
    public class ReviewProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public static ReviewProjectDto Of(int id, string title, string link)
        {
            return new ReviewProjectDto
            {
                Id = id,
                Title = title,
                Link = link
            };
        }
    }
}