using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }


        public int JewelryId { get; set; }
        public virtual Jewelry Jewelry { get; set; }

        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
