using Microsoft.EntityFrameworkCore;
using SneakX.API.Models;
using System.Text.Json;

namespace SneakX.API.Data.Seeders
{
    public class ProductSeeder
    {
        public static async Task SeedProductsAsync(SneakXContext context)
        {
            Console.WriteLine("🚀 Seeder started...");

            // حذف كل المنتجات السابقة لتجربة البيانات الجديدة
            await context.Products.ExecuteDeleteAsync();

            try
            {
                // قراءة ملف JSON
                var jsonData = await File.ReadAllTextAsync("Data/Seeders/products.json");
                Console.WriteLine("📂 JSON file loaded.");

                // تحويل JSON إلى Dictionary
                var productDict = JsonSerializer.Deserialize<Dictionary<string, Product>>(jsonData);

                if (productDict != null)
                {
                    var products = productDict.Values.ToList();

                    foreach (var product in products)
                    {
                        // توليد Slug إذا كان فارغًا
                        if (string.IsNullOrWhiteSpace(product.Slug) && !string.IsNullOrWhiteSpace(product.Name))
                        {
                            product.Slug = product.Name.ToLower().Replace(" ", "-");
                        }
                    }

                    Console.WriteLine($"📦 Found {products.Count} products.");

                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();

                    Console.WriteLine("✅ Products inserted successfully.");
                }
                else
                {
                    Console.WriteLine("⚠️ No products found in the JSON file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error while adding data: {ex.Message}");
            }
        }
    }
}
