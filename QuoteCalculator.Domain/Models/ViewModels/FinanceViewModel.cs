namespace QuoteCalculator.Domain.Models.ViewModels
{
    public class FinanceViewModel
    {
        public Guid Id { get; set; }
        public decimal FinanceAmount { get; set; }
        public string? RepaymentSchedule { get; set; }
        public QuotationModel? Quotation { get; set; }
        public ProductModel? Product { get; set; }

        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }

        public decimal TotalRepayment { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal Interest { get; set; }
    }
}
