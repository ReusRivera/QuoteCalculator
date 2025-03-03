using QuoteCalculator.Services.BorrowerService;
using QuoteCalculator.Services.EmailService;
using QuoteCalculator.Services.FinanceService;
using QuoteCalculator.Services.LoanApplicationService;
using QuoteCalculator.Services.ProductService;
using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.WebApi.ApplicationRegistration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBorrower, Borrower>();
            services.AddScoped<IEmail, Email>();
            services.AddScoped<IFinance, Finance>();
            services.AddScoped<ILoanApplication, LoanApplication>();
            services.AddScoped<IProduct, Product>();
            services.AddScoped<IQuotation, Quotation>();

            return services;
        }

        //public static IServiceCollection AddApplicationServices2(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(MappingProfile)); ;

        //    return services;
        //}

        //public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddAuthentication()
        //            .AddJwtBearer(options =>
        //            {
        //                options.RequireHttpsMetadata = false;
        //                options.SaveToken = true;

        //                options.TokenValidationParameters = new TokenValidationParameters
        //                {
        //                    ValidateIssuerSigningKey = true,
        //                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value)),
        //                    ValidateIssuer = true,
        //                    ValidateAudience = true,
        //                    ValidateLifetime = true,
        //                    ClockSkew = TimeSpan.Zero,
        //                    ValidIssuer = configuration.GetSection("JWT:ValidIssuer").Value,
        //                    ValidAudience = configuration.GetSection("JWT:ValidAudience").Value
        //                };

        //                options.Events = new JwtBearerEvents
        //                {
        //                    OnMessageReceived = context =>
        //                    {
        //                        context.Token = context.Request.Cookies["Token"];
        //                        return Task.CompletedTask;
        //                    }
        //                };
        //            });

        //    return services;
        //}
    }
}
