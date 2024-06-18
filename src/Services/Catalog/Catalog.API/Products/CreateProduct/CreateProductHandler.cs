using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<String> Category, string Description, string ImageFile, decimal Price ) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("The product name is required.")
                .Length(3, 50).WithMessage("The product name must be between 3 and 50 characters.");

            RuleFor(command => command.Category)
                .NotEmpty().WithMessage("At least one category is required.")
                .Must(category => category.All(cat => !string.IsNullOrEmpty(cat))).WithMessage("Each category must be non-empty.")
                .Must(category => category.Count <= 5).WithMessage("A maximum of 5 categories are allowed.");

            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("The product description is required.")
                .MaximumLength(500).WithMessage("The product description must not exceed 500 characters.");

            RuleFor(command => command.ImageFile)
                .NotEmpty().WithMessage("An image file is required.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("The product price must be greater than zero.");
        }
    }

    internal class CreateProductCommanderHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            session.Store( product );
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);

        }
    }
}
