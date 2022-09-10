using System.ComponentModel.DataAnnotations;

namespace Recademy.Core.Models;

public class Settings
{
    [Key]
    public string Token { get; set; }
}