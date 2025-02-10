using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.EmailService;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmail _email;

        public Quotation(ApplicationDbContext context, IEmail email)
        {
            _context = context;
            _email = email;
        }

        public async Task<QuotationModel> AddQuotation(QuotationModel quotation)
        {
            var result = _context.Quotation.Add(quotation);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task CalculateQuote()
        {

        }

        public async Task<bool> ApplyLoan(QuotationDto quotation)
        {
            if (!await IsApplicantEligible(quotation))
                return false;

            return true;
        }

        public async Task<bool> IsApplicantEligible(QuotationDto quotation)
        {
            if (IsApplicantOfLegalAge(quotation.DateOfBirth))
                return false;

            if (await IsMobileNumberBlacklisted(quotation.Mobile))
                return false;

            if (await _email.IsEmailAllowed(quotation.Email))
                return false;

            return true;
        }

        private static bool IsApplicantOfLegalAge(DateOnly birthDate)
        {
            return DateOnly.FromDateTime(DateTime.Today) >= birthDate.AddYears(18);
        }

        private async Task<bool> IsMobileNumberBlacklisted(string mobileNumber)
        {
            return await _context.MobileNumber
                .AnyAsync(e => !e.IsAllowed && e.MobileNumber.Equals(mobileNumber, StringComparison.OrdinalIgnoreCase));
        }
    }
}
