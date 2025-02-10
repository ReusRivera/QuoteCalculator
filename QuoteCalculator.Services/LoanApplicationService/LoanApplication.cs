using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.LoanApplicationService
{
    public class LoanApplication : ILoanApplication
    {
        private readonly ApplicationDbContext _context;

        public LoanApplication(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
