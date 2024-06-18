using BuildingBlocks.Exceptions;

namespace Catalog.API.NewFolder
{
    [Serializable]
    internal class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id )
        {
        }
    }
}