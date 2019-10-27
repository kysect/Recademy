using System;

namespace Recademy.Dto
{
    public class ReviewProjectDto
    {
        public ReviewProjectDto(DateTime requesTime, string description, string title, string link)
        {
            RequesTime = requesTime;
            Description = description;
            Title = title;
            Link = link;
        }

        public DateTime RequesTime { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}