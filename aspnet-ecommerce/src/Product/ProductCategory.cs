using System.ComponentModel.DataAnnotations;

namespace aspnet_ecommerce.src.Product
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

    }
}
