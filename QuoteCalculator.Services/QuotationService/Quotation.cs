using AutoMapper;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Domain.Models.ViewModels;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.FinanceService;

namespace QuoteCalculator.Services.QuotationService
{
    public class Quotation : IQuotation
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Quotation> _logger;
        private readonly IMapper _mapper;
        private readonly IBorrower _borrower;
        private readonly IFinance _finance;

        public Quotation(ApplicationDbContext context, ILogger<Quotation> logger, IMapper mapper, IBorrower borrower, IFinance finance)
        {
            _context = context;
            _logger = logger;
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

        public async Task<QuotationDto?> CreateQuotation(QuotationDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(model);
                var borrowerMap = _mapper.Map<BorrowerModel>(model);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    _logger.LogError("CreateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    _logger.LogError("CreateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return _mapper.Map<QuotationDto>(quotation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<QuotationViewModel?> UpdateQuotation(QuotationViewModel viewModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(viewModel);
                var borrowerMap = _mapper.Map<BorrowerModel>(viewModel);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    _logger.LogError("UpdateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    _logger.LogError("UpdateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return _mapper.Map<QuotationViewModel>(quotation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public async Task<FinanceViewModel?> CalculateQuotation(QuotationViewModel viewModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var quotationMap = _mapper.Map<QuotationModel>(viewModel);
                var borrowerMap = _mapper.Map<BorrowerModel>(viewModel);

                var borrower = await _borrower.ValidateNewBorrower(borrowerMap);

                if (borrower == null)
                {
                    _logger.LogError("CalculateQuotation: Failed to create borrower.");
                    await transaction.RollbackAsync();

                    return null;
                }

                quotationMap.Borrower = borrower;

                var quotation = await AddQuotation(quotationMap);

                if (quotation == null)
                {
                    _logger.LogError("CalculateQuotation: Failed to create quotation.");
                    await transaction.RollbackAsync();

                    return null;
                }

                var finance = await _finance.CreateFinance(quotation, viewModel.Product);

                if (finance == null)
                {
                    _logger.LogError("CalculateQuotation: Failed to create finance.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return _mapper.Map<FinanceViewModel>(finance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CalculateQuotation: An error occurred while creating quotation.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        // Mock CalculateQuotationMock method for research and scientific purposes.
        public async Task<FinanceViewModel?> CalculateQuotationMock(QuotationViewModel viewModel)
        {
            var finance = _finance.CreateFinanceMock();

            return _mapper.Map<FinanceViewModel>(finance);
        }
    }
}
