using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models
{
    public class JewelryType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TypeName { get; set; }

        public ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
    }
}
