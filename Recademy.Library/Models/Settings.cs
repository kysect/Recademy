using System.ComponentModel.DataAnnotations;

namespace Recademy.Library.Models
{
    public class Settings
    {
        [Key]
        public string Token { get; set; }
    }
}