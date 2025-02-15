using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                    //_logger.LogError("CreateProduct: Failed to create product.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return product;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "CreateProduct: An error occurred while creating product.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<List<ProductModel>?> GetAllProductList()
        {
            return await _context.Product
                .ToListAsync();
        }
    }
}
