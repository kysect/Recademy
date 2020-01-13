using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Models
{
    public class Settings
    {
        [Key]
        public string Token { get; set; }
    }
}