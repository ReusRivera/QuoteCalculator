using Microsoft.Extensions.DependencyInjection;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Infrastructure
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBorrowers, Borrowers>();
            services.AddScoped<IQuotation, Quotation>();

            return services;
        }
    }
}
