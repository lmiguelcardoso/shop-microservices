namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public static Product Create(ProductId Id, string Name, decimal Price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Price);

            var product = new Product
            {
                Id = Id,
                Name = Name,
                Price = Price
            };

            return product;
        }
    }
}
