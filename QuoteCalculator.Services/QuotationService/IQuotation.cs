using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.QuotationService
{
    public interface IQuotation
    {
        Task<QuotationModel> AddQuotation(QuotationModel quotation);
    }
}
