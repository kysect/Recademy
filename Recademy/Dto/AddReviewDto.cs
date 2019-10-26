using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class AddReviewDto
    {
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string ReviewText { get; set; }
    }
}
