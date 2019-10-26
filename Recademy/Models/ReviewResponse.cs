using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class ReviewResponse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ReviewRequest")]
        public int ReviewRequestId { get; set; }

        public string Description { get; set; }

        public ReviewRequest ReviewRequest { get; set; }


    }
}
