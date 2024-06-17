
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    //public DeleteProductRequest()
    public record DeleteProductResponse(bool IsSucess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommander(Id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })  
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product"); ;
        }
    }
}
