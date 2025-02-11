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

        private async Task<BorrowerModel> AddBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrower.Add(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<BorrowerModel> UpdateBorrower(BorrowerModel borrower)
        {
            var result = _context.Borrower.Update(borrower);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        //private async Task<BorrowerModel> UpdateBorrowerDeails(BorrowerModel existing, BorrowerModel updated)
        //{
        //    existing.DateModified = DateTime.UtcNow;

        //    return await UpdateBorrower(borrower);
        //}

        private async Task<BorrowerModel> UpdateBorrowerDeails(BorrowerModel borrower)
        {
            borrower.DateModified = DateTime.UtcNow;

            return await UpdateBorrower(borrower);
        }

        private async Task<BorrowerModel?> GetExistingBorrowerByDetails(BorrowerModel borrower)
        {
            return await _context.Borrower
                .FirstOrDefaultAsync(b =>
                    b.FirstName != null && b.FirstName.Equals(borrower.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    b.LastName != null && b.LastName.Equals(borrower.LastName, StringComparison.OrdinalIgnoreCase) &&
                    b.DateOfBirth == borrower.DateOfBirth);
        }

        public async Task<BorrowerModel> ValidateNewBorrower(BorrowerModel borrower)
        {
            var existingBorrower = await GetExistingBorrowerByDetails(borrower);

            if (existingBorrower == null)
                return await AddBorrower(borrower);

            return await UpdateBorrowerDeails(borrower);
        }
    }
}
