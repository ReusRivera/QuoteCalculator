using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;

        public Quotation(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CalculateQuote()
        {

        }

        public async Task ApplyLoan()
        {

        }
    }
}
