using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.BorrowerService
{
    public interface IBorrower
    {
        Task<BorrowerModel> ValidateNewBorrower(BorrowerModel borrower);
    }
}
