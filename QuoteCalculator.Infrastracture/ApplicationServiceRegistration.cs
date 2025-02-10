using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
//using QuoteCalculator.Services.QuotationService;

namespace QuoteCalculator.Infrastructure
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IBorrowers, Borrowers>();
            //services.AddScoped<ILoanApplication, LoanApplication>();
            //services.AddScoped<IQuotation, Quotation>();

            return services;
        }

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
        //                        context.Token = context.Request.Cookies["FrameWorkToken"];
        //                        return Task.CompletedTask;
        //                    }
        //                };
        //            });

        //    return services;
        //}
    }
}
