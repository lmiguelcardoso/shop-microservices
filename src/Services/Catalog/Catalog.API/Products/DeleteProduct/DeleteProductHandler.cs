
using Catalog.API.Products.UpdateProduct;
using FluentValidation;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommander(Guid Id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucess);

    public class DeleteProductCommanderValidator : AbstractValidator<DeleteProductCommander>
    {
        public DeleteProductCommanderValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }

    internal class DeleteProductCommanderHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommander, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommander command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
