using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
    internal class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(object key) : base("Order", key)
        {
        }
    }
}
