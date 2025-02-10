using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.BorrowersService
{
    public interface IBorrowers
    {
        Task<List<BorrowerModel>> GetAllBorrowersList();
    }
}
