using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.Domain.Models.ViewModels
{
    public class QuotationViewModel3
    {
        [Required]
        public decimal AmountRequired { get; set; } = 1;

        [Required]
        public int Term { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Title length can't be more than 10.")]
        public string? Title { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First Name length can't be more than 20.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name length can't be more than 20.")]
        public string? LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Mobile { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        public BorrowerViewModel? Borrower { get; set; }

        public string? RepaymentSchedule { get; set; } = "Monthly";
    }

    public class QuotationViewModel
    {
        //[Required]
        public decimal AmountRequired { get; set; } = 1;

        //[Required]
        public int Term { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Title length can't be more than 10.")]
        public string? Title { get; set; }

        //[Required]
        [StringLength(20, ErrorMessage = "First Name length can't be more than 20.")]
        public string? FirstName { get; set; }

        //[Required]
        [StringLength(20, ErrorMessage = "Last Name length can't be more than 20.")]
        public string? LastName { get; set; }

        //[Required]
        public DateTime? DateOfBirth { get; set; }

        //[Required]
        public string? Mobile { get; set; }

        //[Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        public string? RepaymentSchedule { get; set; } = "Monthly";
    }
}
