using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using QuoteCalculator.WebApi.Controllers;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Services.QuotationService;
using QuoteCalculator.Services.FinanceService;

namespace QuoteCalculator.Tests.Controllers
{
    public class QuotationControllerTests
    {
        private readonly QuotationController _quotationController;

        public QuotationControllerTests()
        {
            var quotation = A.Fake<IQuotation>();
            var mapper = A.Fake<IMapper>();
            var finance = A.Fake<IFinance>();

            _quotationController = new QuotationController(mapper, quotation, finance);
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
