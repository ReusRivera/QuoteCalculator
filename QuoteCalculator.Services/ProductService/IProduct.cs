using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;

namespace QuoteCalculator.Services.ProductService
{
    public interface IProduct
    {
        Task<ProductModel?> CreateProduct(ProductDto model);
    }
}
