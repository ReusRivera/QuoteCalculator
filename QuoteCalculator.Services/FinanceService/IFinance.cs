using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.FinanceService
{
    public interface IFinance
    {
        bool IsFinanceValid(FinanceModel finance);
        Task<FinanceModel?> CreateFinance(FinanceModel finance);
        Task<FinanceModel?> GetFinanceById(Guid? financeId);

        FinanceModel CreateFinanceMock();
    }
}
