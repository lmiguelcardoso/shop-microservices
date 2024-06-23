using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(order => order.OrderName.Value)
            .ToListAsync(cancellationToken);

            var ordersDto = orders.ToOrderDtoList();

            return new GetOrdersByNameResult(ordersDto);
        }
    }
}
