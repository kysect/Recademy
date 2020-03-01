using System.ComponentModel.DataAnnotations;

namespace Recademy.BlazorWeb.Models
{
    public class Settings
    {
        [Key]
        public string Token { get; set; }
    }
}