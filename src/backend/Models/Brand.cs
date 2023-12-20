using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Index(nameof(Name), IsUnique = true)]
public class Brand {
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Car> Cars { get; set; }
}