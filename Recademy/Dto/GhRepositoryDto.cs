using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class GhRepositoryDto
    {
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }

        public override string ToString()
        {
            return RepositoryName;
        }
    }
}
