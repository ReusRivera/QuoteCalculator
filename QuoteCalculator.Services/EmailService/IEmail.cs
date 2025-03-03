namespace QuoteCalculator.Services.EmailService
{
    public interface IEmail
    {
        Task<bool> IsEmailAllowed(string email);
    }
}
