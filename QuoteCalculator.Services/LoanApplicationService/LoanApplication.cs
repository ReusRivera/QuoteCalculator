using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.EmailService;

namespace QuoteCalculator.Services.LoanApplicationService
{
    public class LoanApplication : ILoanApplication
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmail _email;

        public LoanApplication(ApplicationDbContext context, IEmail email)
        {
            _context = context;
            _email = email;
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

        private static bool IsApplicantOfLegalAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return false;

            return DateTime.Today >= birthDate.Value.AddYears(18);
        }

        private async Task<bool> IsMobileNumberBlacklisted(string mobileNumber)
        {
            return await _context.MobileNumber
                .AnyAsync(e => !e.IsAllowed && e.MobileNumber.Equals(mobileNumber, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ApplyLoanApplication(QuotationDto quotation)
        {
            if (!await IsApplicantEligible(quotation))
                return false;

            return true;
        }
    }
}
