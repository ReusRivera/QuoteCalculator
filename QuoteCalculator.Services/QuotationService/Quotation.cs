using AutoMapper;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.FinanceService;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBorrower _borrower;
        private readonly IFinance _finance;

        public Quotation(ApplicationDbContext context, IMapper mapper, IBorrower borrower, IFinance finance)
        {
            _context = context;
            _mapper = mapper;
            _borrower = borrower;
            _finance = finance;
        }

        private async Task<QuotationModel> AddQuotation(QuotationModel quotation)
        {
            var result = _context.Quotation.Add(quotation);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<QuotationModel?> CreateQuotation(QuotationDto model)
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

        public async Task<QuotationModel?> UpdateQuotation(QuotationDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(model);
                var borrowerMap = _mapper.Map<BorrowerModel>(model);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    //_logger.LogError("UpdateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    //_logger.LogError("UpdateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return quotation;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "UpdateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<FinanceModel?> CalculateQuotation(QuotationDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(model);
                var borrowerMap = _mapper.Map<BorrowerModel>(model);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    //_logger.LogError("CalculateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    //_logger.LogError("CalculateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                var finance = await _finance.CreateFinance(quotation, model.Product);

                if (finance == null)
                {
                    //_logger.LogError("CalculateQuotation: Failed to create finance.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return finance;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "CalculateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }
    }
}
