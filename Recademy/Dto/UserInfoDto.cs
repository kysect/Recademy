using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class UserInfoDto
    {
        public string userId { get; set; }
        public ProjectsDto Projects { get; set; }
    }
}
