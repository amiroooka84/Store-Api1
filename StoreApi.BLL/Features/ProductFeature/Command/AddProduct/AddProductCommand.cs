using MediatR;
using StoreApi.Entity._Product;


namespace StoreApi.BLL.Features.ProductFeature.Command.AddProduct
{
    public class AddProductCommand : IRequest<Product>
    {
        public Product Product { get; set; }
        public List<ProductColors> Colors { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ImagesPath { get; set; }
    }
}
