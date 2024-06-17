namespace Catalog.API.NewFolder
{
    [Serializable]
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found!")
        {
        }
    }
}