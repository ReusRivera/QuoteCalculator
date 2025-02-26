using AutoMapper;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;
using QuoteCalculator.Domain.Models.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuotationDto, QuotationModel>();
        CreateMap<QuotationModel, QuotationDto>();

        CreateMap<QuotationViewModel, QuotationModel>();
        CreateMap<QuotationModel, QuotationViewModel>();

        CreateMap<QuotationDto, BorrowerModel>();
        CreateMap<QuotationViewModel, BorrowerModel>();

        CreateMap<BorrowerModel, BorrowerViewModel>();
        CreateMap<BorrowerViewModel, BorrowerModel>();

        CreateMap<ProductDto, ProductModel>();
        CreateMap<ProductModel, ProductDto>();

        CreateMap<FinanceModel, FinanceViewModel>();
        CreateMap<FinanceViewModel, FinanceModel>();

        CreateMap<FinanceViewModel, QuotationModel>();
    }
}
