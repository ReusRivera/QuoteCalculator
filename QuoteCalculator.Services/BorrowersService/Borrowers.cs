using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.BorrowersService
{
    public class Borrowers : IBorrowers
    {
        private readonly ApplicationDbContext _context;

        public Borrowers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowerModel> AddBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrowers.Add(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<BorrowerModel> UpdateBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrowers.Update(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> DeleteBorrower(Guid Id)
        {
            var filteredData = _context.Borrowers
                .FirstOrDefault(x => x.Id == Id);

            _context.Borrowers.Remove(filteredData);

            return await _context.SaveChangesAsync();
        }

        public async Task<BorrowerModel?> GetBorrowerById(Guid Id)
        {
            return await _context.Borrowers
                .FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<List<BorrowerModel>> GetBorrowersList()
        {
            return await _context.Borrowers
                .ToListAsync();
        }
    }
}
