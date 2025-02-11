using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.EmailService;
using QuoteCalculator.Services.LoanApplicationService;

namespace QuoteCalculator.Tests.Services.LoanApplicationService
{
    public class LoanApplicationTests
    {
        private readonly ApplicationDbContext _context;
        private readonly LoanApplication _loanApplicationService;
        private readonly IEmail _email;

        public LoanApplicationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            
            _context = new ApplicationDbContext(options);

            _email = A.Fake<IEmail>();
            _loanApplicationService = new LoanApplication(_context, _email);
        }

        [Fact]
        public async Task IsApplicantEligible_Return_True()
        {
            // Arrange
            var quotationDto = A.Fake<QuotationDto>();

            // Act
            var result = await _loanApplicationService.IsApplicantEligible(quotationDto);

            // Assert
            result.Should().BeTrue();
        }
    }
}
