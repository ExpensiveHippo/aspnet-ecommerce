using System.ComponentModel.DataAnnotations;

namespace aspnet_ecommerce.src.Product
{
    public class ProductBrand
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }
    }
}
