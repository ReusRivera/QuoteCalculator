using QuoteCalculator.Domain.Models;

namespace QuoteCalculator.Services.QuotationService
{
    public interface IQuotation
    {
        Task<QuotationModel?> GetQuotationById(Guid? quotationId);
        bool IsQuotationValid(QuotationModel quotation);
        Task<QuotationModel?> ValidateQuotation(QuotationModel quotation);
        //Task<QuotationModel?> VerifyQuotation(QuotationModel quotation);
    }
}
