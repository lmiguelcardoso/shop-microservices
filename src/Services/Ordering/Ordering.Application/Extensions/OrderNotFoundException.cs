using BuildingBlocks.Exceptions;

namespace Ordering.Application.Extensions
{
    internal class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(object key) : base("Order", key)
        {
        }
    }
}
