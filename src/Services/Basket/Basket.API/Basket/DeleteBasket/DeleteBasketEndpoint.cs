﻿
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);

    public record DeleteBasketResponse(bool IsSucess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}", async (string Username, ISender sender) => 
            {
                var result = await sender.Send(new DeleteBasketCommand(Username));

                var response = result.Adapt<DeleteBasketResponse>();    

                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket"); ;
        }
    }
}