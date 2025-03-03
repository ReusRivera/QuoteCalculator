using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Infrastructure.Data;

namespace QuoteCalculator.Services.EmailService
{
    public class Email : IEmail
    {
        private readonly ApplicationDbContext _context;

        public Email(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<bool> IsEmailDomainBlacklisted(string domain)
        {
            return await _context.EmailDomain
                .AnyAsync(e => !e.IsAllowed && e.DomainName.Equals(domain, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsEmailAllowed(string email)
        {
            string? domain = email?.Split('@').LastOrDefault()?.Trim().ToLower();

            if (string.IsNullOrEmpty(domain))
                return false;

            return !await IsEmailDomainBlacklisted(domain);
        }
    }
}
