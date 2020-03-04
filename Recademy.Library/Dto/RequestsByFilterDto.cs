﻿using System.Collections.Generic;

namespace Recademy.Library.Dto
{
    public class ReviewRequestSearchContextDto
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public int? AuthorId { get; set; }
        public List<string> Tags { get; set; }
    }
}
