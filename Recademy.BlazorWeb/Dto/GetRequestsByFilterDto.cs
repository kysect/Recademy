using System.Collections.Generic;

namespace Recademy.BlazorWeb.Dto
{
    public class GetRequestsByFilterDto
    {
        public int UserId { get; set; }
        public List<string> Tags { get; set; }
    }
}
