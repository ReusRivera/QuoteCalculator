using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Quotation> _logger;
        private readonly IBorrower _borrower;

        public Quotation(ApplicationDbContext context, ILogger<Quotation> logger, IBorrower borrower)
        {
            _context = context;
            _logger = logger;
            _borrower = borrower;
        }

        private async Task<QuotationModel> AddQuotation(QuotationModel quotation)
        {
            var result = _context.Quotation.Add(quotation);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<QuotationModel> UpdateQuotation(QuotationModel quotation)
        {
            var result = _context.Quotation.Update(quotation);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<QuotationModel> UpdateQuotationDetails(QuotationModel quotation)
        {
            quotation.DateModified = DateTime.UtcNow;

            return await UpdateQuotation(quotation);
        }

        private async Task<QuotationModel?> GetQuotationByDetails(QuotationModel quotation)
        {
            return await _context.Quotation
                .FirstOrDefaultAsync(q =>
                    q.Id == quotation.Id ||
                    q.AmountRequired == quotation.AmountRequired &&
                    q.Term == quotation.Term);
        }

        public bool IsQuotationValid(QuotationModel quotation)
        {
            if (quotation == null)
                return false;

            if (quotation.AmountRequired <= 0 || quotation.Term <= 0)
                return false;

            if (quotation.Borrower == null || !_borrower.IsBorrowerDetailsValid(quotation.Borrower))
                return false;

            return true;
        }

        public async Task<QuotationModel?> ValidateQuotation(QuotationModel quotation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingQuotation = await GetQuotationByDetails(quotation);
                var borrower = await _borrower.ValidateBorrower(quotation.Borrower);

                if (existingQuotation == null)
                {
                    quotation.Borrower = borrower;
                    quotation = await AddQuotation(quotation);
                }
                else
                {
                    existingQuotation.Borrower = borrower;
                    quotation = await UpdateQuotationDetails(existingQuotation);
                } 

                await transaction.CommitAsync();

                return quotation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        //public async Task<QuotationModel?> VerifyQuotation(QuotationModel quotation)
        //{
        //    var quotationResult = await GetQuotationByDetails(quotation);

        //    if (quotationResult == null)
        //    {
        //        _logger.LogWarning("UpdateQuotation: An error occurred while updating quotation. Either Quotation or Borrower doesn't exists!");

        //        return null;
        //    }

        //    return quotationResult;
        //}
    }
}
