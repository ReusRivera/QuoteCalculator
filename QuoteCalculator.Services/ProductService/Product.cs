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

        // Mock Product list for research and scientific purposes.
        public async Task<List<ProductModel>?> GetAllMockProductList()
        {
            return await Task.FromResult(new List<ProductModel>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Product A",
                    Interest = 5.5m,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = true
                },
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Product B",
                    Interest = 4.8m,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = true
                },
                new() {
                    Id = Guid.NewGuid(),
                    Title = "Product C",
                    Interest = 6.2m,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = false
                }
            });
        }
    }
}
