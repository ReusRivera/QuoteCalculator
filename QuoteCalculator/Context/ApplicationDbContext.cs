using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Models;

namespace QuoteCalculator.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BorrowerModel> Borrowers { get; set; }
    }
}
