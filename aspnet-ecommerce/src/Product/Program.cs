using aspnet_ecommerce.src.Product;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("ProductDB");
        Console.WriteLine($"Connection String: {connectionString}");

        builder.Services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(connectionString)
        );

        builder.Services.AddScoped<ProductItemSeeder>();

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProductItemSeeder>();
            await context.SeedAsync();
        }
    }   
}