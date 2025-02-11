using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.EmailService;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBorrower _borrower;
        private readonly IEmail _email;

        public Quotation(ApplicationDbContext context, IMapper mapper, IBorrower borrower, IEmail email)
        {
            _context = context;
            _mapper = mapper;
            _borrower = borrower;
            _email = email;
        }

        private async Task<QuotationModel> AddQuotation(QuotationModel quotation)
        {
            var result = _context.Quotation.Add(quotation);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<bool> IsApplicantEligible(QuotationDto quotation)
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

        public async Task<QuotationModel?> CreateQuotationAsync(QuotationDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(model);
                var borrowerMap = _mapper.Map<BorrowerModel>(model);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    //_logger.LogError("CreateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    //_logger.LogError("CreateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return quotation;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "CreateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<QuotationModel?> CalculateQuotationAsync(QuotationDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(model);
                var borrowerMap = _mapper.Map<BorrowerModel>(model);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    //_logger.LogError("CreateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    //_logger.LogError("CreateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return quotation;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "CreateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<bool> ApplyLoan(QuotationDto quotation)
        {
            if (!await IsApplicantEligible(quotation))
                return false;

            return true;
        }
    }
}
