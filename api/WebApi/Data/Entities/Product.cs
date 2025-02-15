using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Entities;

public class Product
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(4000)]
    public string Description { get; set; }

    public bool IsSold { get; set; }
}
