namespace QuoteCalculator.Domain.Models
{
    public class QuotationModel
    {
        public Guid Id { get; set; } = new Guid();
        public int Amount { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
    }
}
