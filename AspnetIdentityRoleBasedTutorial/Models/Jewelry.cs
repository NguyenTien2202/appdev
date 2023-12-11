using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AspnetIdentityRoleBasedTutorial.Models
{
    public class Jewelry
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? JewelryName { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string FormattedPrice
        {
            get
            {
                return Price.ToString("N0", CultureInfo.CurrentCulture);
            }
        }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        
        public int CategoryId { get; set; }
        public int JewelryTypeId { get; set; }
        public Category? Category { get; set; }
        public JewelryType? JewelryType { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
        [NotMapped]
        public string? TypeName { get; set; }

    }
}
