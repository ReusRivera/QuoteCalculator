using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.FinanceService;
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
            var mapper = A.Fake<IMapper>();
            var borrower = A.Fake<IBorrower>();
            var finance = A.Fake<IFinance>();

            _quotationService = new Quotation(context, mapper, borrower, finance);
        }

        [Fact]
        public async Task CreateQuotation_Return_QuotationModel()
        {
            // Arrange
            var quotationDto = A.Fake<QuotationDto>();

            // Act
            var result = await _quotationService.CreateQuotation(quotationDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<QuotationModel>();
        }
    }
}
