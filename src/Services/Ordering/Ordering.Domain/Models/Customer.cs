namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public static Customer Create(CustomerId Id, string Name, string Email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Name);
            ArgumentException.ThrowIfNullOrWhiteSpace(Email);

            var customer = new Customer
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            return customer;
        }
    }
}
