using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Caffe.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? AvatarPath { get; set; }

        public string role { get; set; }
    }
}
