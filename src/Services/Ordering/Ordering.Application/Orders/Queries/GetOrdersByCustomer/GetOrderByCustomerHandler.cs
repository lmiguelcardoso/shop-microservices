
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                        .Include(o => o.OrderItems)
                        .AsNoTracking()
                        .Where(order => order.CustomerId.Equals(CustomerId.Of(query.CustomerId)))
                        .OrderBy(order => order.OrderName.Value)
                        .ToListAsync(cancellationToken);

            return new GetOrderByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
