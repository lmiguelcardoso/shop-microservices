
using Catalog.API.NewFolder;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryResult called with {}", query);

            var products = await session.Query<Product>()
                .Where(product=> product.Category.Contains(query.Category))
                .ToListAsync();

            return new GetProductByCategoryResult(products);
        }
    }
}
