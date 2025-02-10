namespace QuoteCalculator.Domain.Models
{
    public class BorrowerModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Mobile { get; set; }
        public required string Email { get; set; }
    }
}
