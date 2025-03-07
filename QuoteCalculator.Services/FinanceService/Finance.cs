using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.ViewModels;
using QuoteCalculator.Infrastructure.Data;
using QuoteCalculator.Services.ProductService;

namespace QuoteCalculator.Services.FinanceService
{
    public class Finance : IFinance
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Finance> _logger;
        private readonly IProduct _product;

        public Finance(ApplicationDbContext context, ILogger<Finance> logger, IProduct product)
        {
            _context = context;
            _logger = logger;
            _product = product;
        }

        private IQueryable<FinanceModel> GetIQueryableFinance()
        {
            return _context.Finance
                .Include(f => f.Quotation)
                    .ThenInclude(q => q.Borrower)
                .Include(f => f.Product);
        }

        private async Task<FinanceModel> AddFinance(FinanceModel finance)
        {
            var result = _context.Finance.Add(finance);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public bool IsFinanceValid(FinanceModel finance)
        {
            if (finance == null)
                return false;

            if (finance.Quotation == null || finance.Product == null)
                return false;

            if (string.IsNullOrWhiteSpace(finance.RepaymentSchedule))
                return false;

            return true;
        }

        private async Task ValidateFinanceProduct(FinanceModel finance)
        {
            var product = await _product.GetProductById(finance.Product);

            if (product == null)
            {
                string message = "Product doesn't exists.";

                _logger.LogWarning($"ValidateFinanceProduct: {message}");

                throw new ArgumentException(message);
            }

            finance.Product = product;
        }

        public async Task<FinanceModel?> GetFinanceById(Guid? financeId)
        {
            return await GetIQueryableFinance()
                .FirstOrDefaultAsync(f => f.Id == financeId);
        }

        public async Task<FinanceModel?> CreateFinance(FinanceModel finance)
        {
            await ValidateFinanceProduct(finance);

            var amount = finance.Quotation.AmountRequired;
            var term = finance.Quotation.Term;
            var interest = finance.Product.Interest;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal repayment = CalculateRepayment(amount, interest, term, finance.RepaymentSchedule);
                decimal interestAmount = CalculateInterestAmount(amount, term, repayment);

                finance.FinanceAmount = CalculateFinanceAmount(amount, interestAmount);

                var financeResult = await AddFinance(finance);

                await transaction.CommitAsync();
                 
                return financeResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateFinance: An error occurred while creating finance.");
                await transaction.RollbackAsync();

                return null;
            }
        }

        public decimal CalculateRepayment(decimal loanAmount, decimal annualInterestRate, int loanTerms, string repaymentSchedule)
        {
            int periodsPerYear = DetermineRepaymentSchedule(repaymentSchedule);

            if (annualInterestRate == 0)
                return Math.Round(loanAmount / loanTerms, 2);

            decimal periodicRate = annualInterestRate / 100 / periodsPerYear;
            decimal repayment = loanAmount * (periodicRate / (1 - (decimal)Math.Pow(1 + (double)periodicRate, -loanTerms)));

            return Math.Round(repayment, 2);
        }

        public void UpdateFinanceComputation(FinanceViewModel viewModel, FinanceModel finance)
        {
            int loanTerms = finance.Quotation.Term;
            decimal loanAmount = finance.Quotation.AmountRequired;
            decimal repayment = finance.FinanceAmount;

            decimal establishmentFee = CalculateEstablishmentFee();
            decimal interestAmount = CalculateInterestAmount(loanAmount, loanTerms, repayment);

            viewModel.EstablishmentFee = establishmentFee;
            viewModel.Interest = interestAmount;
            viewModel.TotalRepayment = CalculateAmountPayable(loanAmount, interestAmount, establishmentFee);
        }

        private static int DetermineRepaymentSchedule(string repaymentSchedule)
        {
            return repaymentSchedule.ToLower() switch
            {
                "weekly" => 52,
                "monthly" => 12,
                _ => throw new ArgumentException("Invalid frequency. Use 'weekly' or 'monthly'."),
            };
        }

        private static decimal CalculateFinanceAmount(decimal loanAmount, decimal interestAmount)
        {
            return Math.Round(loanAmount + interestAmount, 2);
        }

        private static decimal CalculateEstablishmentFee()
        {
            return 300.00M; // Mock Establishment Fee for research and scientific purposes.
        }

        private static decimal CalculateInterestAmount(decimal loanAmount, int loanTerms, decimal repayment)
        {
            return Math.Round((repayment * loanTerms) - loanAmount, 2);
        }

        private static decimal CalculateAmountPayable(decimal loanAmount, decimal interestAmount, decimal establishmentFee)
        {
            return Math.Round(loanAmount + interestAmount + establishmentFee, 2);
        }

        // Mock FinanceModel for research and scientific purposes.
        public FinanceModel CreateFinanceMock()
        {
            return new FinanceModel
            {
                Id = Guid.NewGuid(),
                FinanceAmount = 50000m,
                RepaymentSchedule = "Weekly",
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
