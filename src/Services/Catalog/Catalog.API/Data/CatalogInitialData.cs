using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync()) return;
            
            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Category = new List<string> { "Electronics", "Mobile" },
                Description = "A high-end smartphone with a large display and powerful processor.",
                ImageFile = "smartphone.jpg",
                Price = 699.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Gaming Laptop",
                Category = new List<string> { "Electronics", "Computers" },
                Description = "A powerful gaming laptop with the latest graphics card.",
                ImageFile = "gaming_laptop.jpg",
                Price = 1299.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Wireless Headphones",
                Category = new List<string> { "Electronics", "Audio" },
                Description = "Noise-cancelling wireless headphones with long battery life.",
                ImageFile = "wireless_headphones.jpg",
                Price = 199.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smartwatch",
                Category = new List<string> { "Electronics", "Wearables" },
                Description = "A stylish smartwatch with health tracking features.",
                ImageFile = "smartwatch.jpg",
                Price = 299.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "4K TV",
                Category = new List<string> { "Electronics", "Home Entertainment" },
                Description = "A 55-inch 4K TV with HDR support.",
                ImageFile = "4k_tv.jpg",
                Price = 799.99m
            }
        };
    }
}
