using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BorrowerModel> Borrowers { get; set; }
    }
}
