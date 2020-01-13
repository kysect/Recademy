using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.BlazorWeb.Models
{
    public class ReviewResponse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ReviewRequest")] 
        public int ReviewRequestId { get; set; }

        [ForeignKey("User")] 
        public int ReviewerId { get; set; }

        public string Description { get; set; }

        public ReviewRequest ReviewRequest { get; set; }

        public DateTime CreationTime { get; set; }
    }
}