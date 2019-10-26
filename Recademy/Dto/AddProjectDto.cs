using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class AddProjectDto
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
    }
}
