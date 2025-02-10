using QuoteCalculator.Infrastracture.Context;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;

        public Quotation(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
