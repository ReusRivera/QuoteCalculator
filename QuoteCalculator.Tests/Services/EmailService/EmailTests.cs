using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.EmailService;

namespace QuoteCalculator.Tests.Services.EmailService
{
    public class EmailTests
    {
        private readonly Email _emailService;

        public EmailTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);

            _emailService = new Email(context);
        }
    }
}
