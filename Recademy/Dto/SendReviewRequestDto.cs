﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class SendReviewRequestDto
    {
        public int ReviewRequestId { get; set; }
        public string ReviewText { get; set; }

        public static SendReviewRequestDto Of(int id, string text)
        {
            return new SendReviewRequestDto()
            {
                ReviewRequestId = id,
                ReviewText = text
            };
        }
    }
}