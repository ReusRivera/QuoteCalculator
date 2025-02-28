using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.ProductService;
using QuoteCalculator.WebApi.Controllers;

namespace QuoteCalculator.Tests.Services.ProductService
{
    public class ProductTests
    {
        private readonly Product _productService;
        private readonly ProductController _productController;

        public ProductTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            var logger = A.Fake<ILogger<Product>>();
            var mapper = A.Fake<IMapper>();
            var product = A.Fake<IProduct>();

            _productService = new Product(context, logger, mapper);
            _productController = new ProductController(product);
        }

        [Fact]
        public async Task GetProductList_Return_OkResult()
        {
            // Arrange

            // Act
            var result = await _productController.GetProductList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllProductList_Return_ListProductModel()
        {
            // Arrange

            // Act
            var result = await _productService.GetAllProductList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ProductModel>>();
        }
    }
}
