﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class GetRequestsByFilterDto
    {
        public int UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
