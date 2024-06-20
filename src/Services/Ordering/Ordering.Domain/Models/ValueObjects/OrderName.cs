namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public Guid Value { get; }
        private OrderName(string name) => Value = Value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }

    }
}
