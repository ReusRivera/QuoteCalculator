using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.QuotationService
{
    public interface IQuotation
    {
        bool IsQuotationValid(QuotationModel quotation);
        Task<QuotationModel?> ValidateQuotation(QuotationModel quotation);
        //Task<QuotationModel?> VerifyQuotation(QuotationModel quotation);
    }
}
