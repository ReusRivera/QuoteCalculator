namespace QuoteCalculator.Models.Dto
{
    public class QuotationDto
    {
        public int AmountRequired { get; set; }
        public int Term { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Mobile { get; set; }
        public required string Email { get; set; }
    }
}
