﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.FinanceService;
using QuoteCalculator.Services.ProductService;

namespace QuoteCalculator.Tests.Services.FinanceService
{
    public class FinanceTests
    {
        private readonly Finance _financeService;

        public FinanceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            var logger = A.Fake<ILogger<Finance>>();
            var product = A.Fake<IProduct>();

            _financeService = new Finance(context, logger, product);
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
            var result = _financeService.CalculateMonthlyRepayment(loanAmount, annualInterestRate, monthlyLoanTerms);

            // Assert
            result.Should().NotBe(default);
            result.Should().Be(expectedRepaymentAmount);
        }
    }
}
