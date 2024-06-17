using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsCommand() : ICommand<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : ICommandHandler<GetProductsCommand, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsCommand query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler called with {}", query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
