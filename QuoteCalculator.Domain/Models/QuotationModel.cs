namespace QuoteCalculator.Domain.Models
{
    public class QuotationModel
    {
        public Guid Id { get; set; } = new Guid();
        public int AmountRequired { get; set; }
        public int Term { get; set; }
        public required BorrowerModel Borrower { get; set; }
        //public required ProductModel Product { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
