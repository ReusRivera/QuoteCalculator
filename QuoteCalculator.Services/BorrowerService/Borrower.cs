using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.BorrowerService
{
    public class Borrower : IBorrower
    {
        private readonly ApplicationDbContext _context;

        public Borrower(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowerModel> AddBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrower.Add(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<BorrowerModel> UpdateBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrower.Update(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> DeleteBorrower(Guid Id)
        {
            var filteredData = _context.Borrower
                .FirstOrDefault(x => x.Id == Id);

            _context.Borrower.Remove(filteredData);

            return await _context.SaveChangesAsync();
        }

        public async Task<BorrowerModel?> GetBorrowerById(Guid Id)
        {
            return await _context.Borrower
                .FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<List<BorrowerModel>> GetBorrowersList()
        {
            return await _context.Borrower
                .ToListAsync();
        }
    }
}
