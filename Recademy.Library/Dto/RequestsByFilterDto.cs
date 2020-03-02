using System.Collections.Generic;

namespace Recademy.Library.Dto
{
    public class RequestsByFilterDto
    {
        public int UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
