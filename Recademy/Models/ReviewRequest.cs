using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Types;

namespace Recademy.Models
{
    public class ReviewRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProjectInfo")]
        public int ProjectId { get; set; }

        public DateTime DateCreate { get; set; }

        public int ReviewerId { get; set; }

        public ProjectState State { get; set; }
        
        public ProjectInfo ProjectInfo { get; set; }

        public User User { get; set; }

        public ReviewResponse ReviewResponse { get; set; }
    }
}
