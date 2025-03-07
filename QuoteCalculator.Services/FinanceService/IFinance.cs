using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.ViewModels;

namespace QuoteCalculator.Services.FinanceService
{
    public interface IFinance
    {
        bool IsFinanceValid(FinanceModel finance);
        Task<FinanceModel?> GetFinanceById(Guid? financeId);
        Task<FinanceModel?> CreateFinance(FinanceModel finance);
        void UpdateFinanceComputation(FinanceViewModel viewModel, FinanceModel finance);

        FinanceModel CreateFinanceMock();
    }
}
