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

        private async Task<BorrowerModel?> GetBorrowerByDetails(BorrowerModel borrower)
        {
            return await _context.Borrower
                .FirstOrDefaultAsync(b =>
                    b.FirstName != null && b.FirstName.ToLower() == borrower.FirstName.ToLower() &&
                    b.LastName != null && b.LastName.ToLower() == borrower.LastName.ToLower() &&
                    b.DateOfBirth.Date == borrower.DateOfBirth.Date);
        }

        public bool IsBorrowerDetailsValid(BorrowerModel borrower)
        {
            if (borrower == null)
                return false;

            if (string.IsNullOrWhiteSpace(borrower.Title) ||
                string.IsNullOrWhiteSpace(borrower.FirstName) ||
                string.IsNullOrWhiteSpace(borrower.LastName) ||
                string.IsNullOrWhiteSpace(borrower.Mobile) ||
                string.IsNullOrWhiteSpace(borrower.Email))
            {
                return false;
            }

            return true;
        }

        public async Task<BorrowerModel> ValidateBorrower(BorrowerModel borrower)
        {
            var existingBorrower = await GetBorrowerByDetails(borrower);

            if (existingBorrower == null)
                return await AddBorrower(borrower);

            return await UpdateBorrowerDeails(existingBorrower);
        }
    }
}
