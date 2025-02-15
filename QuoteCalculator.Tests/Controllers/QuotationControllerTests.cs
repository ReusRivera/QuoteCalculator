using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.WebApi.Controllers;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Tests.Controllers
{
    public class QuotationControllerTests
    {
        private readonly QuotationController _quotationController;

        public QuotationControllerTests()
        {
            var quotation = A.Fake<IQuotation>();
            _quotationController = new QuotationController(quotation);
        }

        [Fact]
        public async Task CreateQuote_Return_Ok()
        {
            // Arrange
            var quotationDto = A.Fake<QuotationDto>();

            // Act
            var result = await _quotationController.CreateQuote(quotationDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
