using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;

namespace QuoteCalculator.Tests.Services.BorrowerService
{
    public class BorrowerTests
    {
        private readonly Borrower _borrowerService;

        public BorrowerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            _borrowerService = new Borrower(context);
        }

        [Fact]
        public async Task ValidateNewBorrower_Return_BorrowerModel()
        {
            // Arrange
            var borrower = new BorrowerModel
            {
                Id = Guid.NewGuid(),
                Title = "Mr.",
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Mobile = "1234567890"
            };

            // Act
            var result = await _borrowerService.ValidateNewBorrower(borrower);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BorrowerModel>();
        }
    }
}
