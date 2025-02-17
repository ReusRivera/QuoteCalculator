namespace QuoteCalculator.Domain.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; } = new Guid();
        public string? Title { get; set; }
        public decimal Interest { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
