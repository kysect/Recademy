using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class AddReviewDto
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ReviewText { get; set; }
    }
}
