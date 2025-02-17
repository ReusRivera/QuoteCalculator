using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Domain.Models.ViewModels;

namespace QuoteCalculator.Services.QuotationService
{
    public interface IQuotation
    {
        Task<QuotationDto?> CreateQuotation(QuotationDto model);
        Task<FinanceViewModel?> CalculateQuotation(QuotationViewModel viewModel);
        Task<FinanceViewModel?> CalculateMockQuotation(QuotationViewModel viewModel);
    }
}
