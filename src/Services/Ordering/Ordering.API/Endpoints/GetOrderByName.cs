using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrderSByNameRequest();
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(orderName);

                var result = await sender.Send(query);

                //var response = result.Adapt<GetOrdersByNameResponse>();
                // @todo fix the type to GetOrdersByNameResponse
                return Results.Ok(result);
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Order By Name")
            .WithDescription("Get Order By Name"); ;
        }
    }
}
