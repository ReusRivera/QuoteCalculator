using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Context;
using QuoteCalculator.Models;

namespace QuoteCalculator.Services.BorrowersService
{
    public class Borrowers : IBorrowers
    {
        private readonly ApplicationDbContext _context;

        public Borrowers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BorrowerModel>> GetAllBorrowersList()
        {
            return await _context.Borrowers
                .ToListAsync();
        }
    }
}
