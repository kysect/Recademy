using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class UserInfoDto
    {
        public string Name { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}
