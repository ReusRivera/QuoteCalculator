namespace QuoteCalculator.Domain.Models
{
    public class LoanApplicationModel
    {
        public Guid Id { get; set; } = new Guid();
        public required FinanceModel Finance { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
