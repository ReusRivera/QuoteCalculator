using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.ViewModels;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Tests.Services.QuotationService
{
    public class QuotationTests
    {
        private readonly Quotation _quotationService;

        public QuotationTests()
        {
            var options1 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var options2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            //var context = new ApplicationDbContext(options1);
            var context = new ApplicationDbContext(options2);

            var logger = A.Fake<ILogger<Quotation>>();
            var borrower = A.Fake<IBorrower>();

            _quotationService = new Quotation(context, logger, borrower);
        }

        [Fact]
        public async Task CreateQuotation_Return_QuotationViewModel()
        {
            // Arrange
            var quotation = A.Fake<QuotationModel>();

            // Act
            var result = await _quotationService.ValidateQuotation(quotation);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<QuotationViewModel>();
        }
    }
}
