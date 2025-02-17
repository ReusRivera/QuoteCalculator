namespace QuoteCalculator.Domain.Models
{
    public class BorrowerModel
    {
        public Guid Id { get; set; } = new Guid();
        public required string Title { get; set; }
        public string? FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Mobile { get; set; }
        public required string Email { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
