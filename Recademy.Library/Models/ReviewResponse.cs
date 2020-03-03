using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recademy.Library.Types;

namespace Recademy.Library.Models
{
    public class ReviewResponse
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        //TODO: implement
        public ReviewConclusion ReviewConclusion { get; set; }

        [ForeignKey("ReviewRequest")] 
        public int ReviewRequestId { get; set; }
        public ReviewRequest ReviewRequest { get; set; }

        [ForeignKey("User")] 
        public int ReviewerId { get; set; }
        public User Reviewer { get; set; }
    }
}