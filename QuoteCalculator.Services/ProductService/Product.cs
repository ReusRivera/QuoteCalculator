using AutoMapper;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.ProductService
{
    public class Product : IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Product(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<ProductModel> AddProduct(ProductModel product)
        {
            var result = _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<ProductModel?> CreateProduct(ProductDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var productMap = _mapper.Map<ProductModel>(model);

                var product = await AddProduct(productMap);

                if (product == null)
                {
                    //_logger.LogError("CreateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return product;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "CreateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }
    }
}
