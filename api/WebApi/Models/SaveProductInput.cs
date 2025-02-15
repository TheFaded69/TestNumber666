using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SaveProductInput
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(4000)]
    public string Description { get; set; }

    public bool IsSold { get; set; }
}
