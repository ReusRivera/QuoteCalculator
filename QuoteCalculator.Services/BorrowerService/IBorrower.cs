using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.BorrowerService
{
    public interface IBorrower
    {
        bool IsBorrowerDetailsValid(BorrowerModel borrower);
        Task<BorrowerModel> ValidateBorrower(BorrowerModel borrower);
    }
}
