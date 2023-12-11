using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
    }
}
