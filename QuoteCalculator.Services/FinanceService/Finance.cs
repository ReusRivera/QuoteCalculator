using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.FinanceService
{
    public class Finance : IFinance
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Finance> _logger;

        public Finance(ApplicationDbContext context, ILogger<Finance> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<FinanceModel> AddFinance(FinanceModel finance)
        {
            var result = _context.Finance.Add(finance);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        private static FinanceModel CreateFinanceModel(QuotationModel quotation, ProductModel product, decimal financeAmount)
        {
            return new FinanceModel()
            {
                FinanceAmount = financeAmount,
                RepaymentSchedule = "Monthly",
                Quotation = quotation,
                Product = product
            };
        }

        public async Task<FinanceModel?> CreateFinance(QuotationModel quotation, ProductModel product)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal decimalAmount = quotation.AmountRequired;

                var repayment = CalculateMonthlyRepayment(decimalAmount, product.Interest, quotation.Term);
                var model = CreateFinanceModel(quotation, product, repayment);

                var finance = await AddFinance(model);

                if (finance == null)
                {
                    _logger.LogError("CreateFinance: Failed to create finance.");
                    await transaction.RollbackAsync();

                    return null;
                }

                await transaction.CommitAsync();

                return finance;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateFinance: An error occurred while creating finance.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public decimal CalculateMonthlyRepayment(decimal loanAmount, decimal annualInterestRate, int monthlyLoanTerms)
        {
            if (annualInterestRate == 0)
                return loanAmount / monthlyLoanTerms;

            decimal monthlyRate = annualInterestRate / 100 / 12;
            decimal monthlyRepayment = loanAmount * (monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -monthlyLoanTerms)));

            return Math.Round(monthlyRepayment, 2);
        }

        // Mock FinanceModel for research and scientific purposes.
        public FinanceModel CreateFinanceMock()
        {
            return new FinanceModel
            {
                Id = Guid.NewGuid(),
                FinanceAmount = 50000m,
                RepaymentSchedule = "Monthly",
                Quotation = new QuotationModel
                {
                    Id = Guid.NewGuid(),
                    AmountRequired = 50000,
                    Term = 12,
                    Borrower = new BorrowerModel
                    {
                        Id = Guid.NewGuid(),
                        Title = "Mr.",
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(1990, 5, 15),
                        Mobile = "1234567890",
                        Email = "johndoe@example.com",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsActive = true
                    },
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = true
                },
                Product = new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Product A",
                    Interest = 5.5m,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = true
                },
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                IsActive = true
            };
        }
    }
}
