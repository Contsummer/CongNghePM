using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caffe.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDonChiTiet { get; set; }
        [Required]
        public int MaHoaDon { get; set; }
        [Required]
        public int DrinkId { get; set; }
        [Required]
        public int Soluong { get; set; }

        [NotMapped] // Do not map this to the database
        public decimal Thanhtien
        {
            get
            {
                return Drink != null ? Soluong * Drink.Price : 0;
            }
        }

        [ForeignKey("MaHoaDon")]
        public virtual HoaDon HoaDon { get; set; }  

        [ForeignKey("DrinkId")]
        public virtual Drink Drink { get; set; }  

    }
}