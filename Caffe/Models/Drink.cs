using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Caffe.Models
{
    public class Drink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DrinkId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18)")]
        public decimal Price { get; set; }
        public string descripton { get; set; }
        public string? imagePath { get; set; }

     //   public HoaDon Invoice { get; set; }
    }
}
