using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace aspnet_ecommerce.src.Product;
public class ProductItemSeeder
{
    private readonly ProductContext _context;

    public ProductItemSeeder(ProductContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // only seed if table is empty
        if (!_context.ProductItems.Any())
        {
            // get dummy data
            var path = "src/Product/products.json";
            var content = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<ProductItemEntry[]>(content);

            // re-initialise categories 
            _context.ProductCategories.RemoveRange(_context.ProductCategories);
            await _context.ProductCategories
                .AddRangeAsync(items.Select(i => i.Category).Distinct()
                .Select(category => new ProductCategory { Category = category }));

            // re-initialise brands
            _context.ProductBrands.RemoveRange(_context.ProductBrands);
            await _context.ProductBrands
                .AddRangeAsync(items.Select(i => i.Brand).Distinct()
                .Select(brand => new ProductBrand { Brand = brand }));

            await _context.SaveChangesAsync();

            var brands = await _context.ProductBrands.ToDictionaryAsync(b => b.Brand, b => b.Id);
            var categories = await _context.ProductCategories.ToDictionaryAsync(c => c.Category, c => c.Id);

            // add items after re-initalising categories and brands so that the ids can be mapped
            await _context.ProductItems.AddRangeAsync(items.Select(item => new ProductItem
            {
                Id = item.Id,
                Name = item.Name,
                ProductCategoryId = categories[item.Category],
                ProductBrandId = brands[item.Brand],
                Description = item.Description,
                Price = item.Price,
                Stock = 100
            }));

            await _context.SaveChangesAsync();
        }
    }

    private class ProductItemEntry
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
