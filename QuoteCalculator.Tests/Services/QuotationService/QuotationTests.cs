using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
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
            var mapper = A.Fake<IMapper>();
            var borrower = A.Fake<IBorrower>();

            _quotationService = new Quotation(context, mapper, borrower);
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

        [Fact]
        public void CalculateMonthlyRepayment_Return_DecimalValue()
        {
            // Arrange
            decimal loanAmount = 100000m;
            decimal annualInterestRate = 5.5m;
            int monthlyLoanTerms = 120;

            decimal expectedRepaymentAmount = 1085.26m;

            // Act
            var result = _quotationService.CalculateMonthlyRepayment(loanAmount, annualInterestRate, monthlyLoanTerms);

            // Assert
            result.Should().NotBe(default);
            result.Should().Be(expectedRepaymentAmount);
        }
    }
}
