using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.FinanceService
{
    public interface IFinance
    {
        Task<FinanceModel?> CreateFinance(QuotationModel quotation, ProductModel product);
        FinanceModel CreateFinanceMock();
    }
}
