using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Dto
{
    public class TagsDto
    {
        public List<string> Tags { get; set; }

        public static TagsDto Of(string tag)
        {
            return new TagsDto()
            {
                Tags = new List<string> { tag }
            };
        }
    }
}
