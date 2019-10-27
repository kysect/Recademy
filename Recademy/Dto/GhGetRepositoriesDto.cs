using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class GhGetRepositoriesDto
    {
        public int UserId { get; set; }

        public static GhGetRepositoriesDto GetGto(int id)
        {
            return new GhGetRepositoriesDto() {UserId = id};
        }
    }
}
