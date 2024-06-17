
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommander(Guid Id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucess);

    internal class DeleteProductCommanderHandler(IDocumentSession session, ILogger<DeleteProductCommanderHandler> logger) : ICommandHandler<DeleteProductCommander, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommander command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommanderHandler called with {}", command);
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
