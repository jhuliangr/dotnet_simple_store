using System.ComponentModel.DataAnnotations;

namespace Store.Client.Models;

public class Thing
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
}