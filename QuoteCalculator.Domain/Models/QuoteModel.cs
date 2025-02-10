namespace QuoteCalculator.Domain.Models
{
    public class QuoteModel
    {
        public Guid Id { get; set; }
        public int AmountRequired { get; set; }
        public int Term { get; set; }
    }
}
