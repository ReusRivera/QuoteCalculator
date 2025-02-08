namespace QuoteCalculator.Models
{
    public class QuoteModel
    {
        public Guid Id { get; set; }
        public int AmountRequired { get; set; }
        public int Term { get; set; }
    }
}
