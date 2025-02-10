using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BorrowerModel> Borrower { get; set; }
        public DbSet<EmailDomainModel> EmailDomain { get; set; }
        public DbSet<LoanApplicationModel> LoanApplication { get; set; }
        public DbSet<MobileNumberModel> MobileNumber { get; set; }
        public DbSet<QuotationModel> Quotation { get; set; }
    }
}
