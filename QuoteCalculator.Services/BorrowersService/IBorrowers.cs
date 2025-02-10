using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.BorrowersService
{
    public interface IBorrowers
    {
        Task<BorrowerModel> AddBorrower(BorrowerModel borrower);
        Task<BorrowerModel> UpdateBorrower(BorrowerModel borrower);
        Task<int> DeleteBorrower(Guid Id);

        Task<BorrowerModel?> GetBorrowerById(Guid Id);
        Task<List<BorrowerModel>> GetBorrowersList();
    }
}
