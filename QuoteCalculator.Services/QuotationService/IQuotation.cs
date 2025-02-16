using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;

namespace QuoteCalculator.Services.QuotationService
{
    public interface IQuotation
    {
        Task<QuotationModel?> CreateQuotation(QuotationDto model);
        Task<FinanceModel?> CalculateQuotation(QuotationDto model);
    }
}
