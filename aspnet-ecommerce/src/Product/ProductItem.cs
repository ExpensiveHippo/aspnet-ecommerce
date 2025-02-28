using aspnet_ecommerce.src.Product;

namespace aspnet_ecommerce;

public class ProductItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProductBrandId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public ProductItem() {}

    public void AddStock(int amount)
    {
        if (amount <= 0)
        {
               throw new Exception("Amount to add must be greater than 0");
        }
        Stock += amount;
    }

    public void RemoveStock(int amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Amount to remove must be greater than 0");
        }

        if (Stock - amount < 0)
        {
            throw new Exception("Not enough stock available"); 
        }
        Stock -= amount;
    }
}
