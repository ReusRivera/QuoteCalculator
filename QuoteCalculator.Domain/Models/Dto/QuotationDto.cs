using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.Domain.Models.Dto
{
    public class QuotationDto
    {
        public int AmountRequired { get; set; }
        public int Term { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Title length can't be more than 10.")]
        public string Title { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First Name length can't be more than 20.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name length can't be more than 20.")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Mobile { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
