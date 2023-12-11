using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetIdentityRoleBasedTutorial.Models
{
    public class JewelryIndexViewModel
    {
        public List<Jewelry>? Jewelries { get; set; }
        public SelectList? Types { get; set; }
        public SelectList? Categories { get; set; }
        public string? JewelryTypeFilter { get; set; }
        public int? CategoryFilter { get; set; }
        public string? SearchString { get; set; }
    }
}
