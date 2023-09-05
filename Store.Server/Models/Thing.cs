using System.ComponentModel.DataAnnotations;

namespace Store.Server.Models;

public class Thing
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
}