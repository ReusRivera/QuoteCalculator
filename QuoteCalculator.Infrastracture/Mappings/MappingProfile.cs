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

        CreateMap<QuotationViewModel, QuotationModel>()
            .ForMember(dest => dest.Borrower, opt => opt.MapFrom(src => src));

        CreateMap<QuotationModel, QuotationViewModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Borrower.Title))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Borrower.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Borrower.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Borrower.DateOfBirth))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Borrower.Mobile))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Borrower.Email));
            //.ForMember(dest => dest.RepaymentSchedule, opt => opt.MapFrom(src => "Weekly")); // Default value

        CreateMap<QuotationDto, BorrowerModel>();
        CreateMap<QuotationViewModel, BorrowerModel>();

        CreateMap<BorrowerModel, BorrowerViewModel>();
        CreateMap<BorrowerViewModel, BorrowerModel>();

        CreateMap<ProductDto, ProductModel>();
        CreateMap<ProductModel, ProductDto>();

        CreateMap<FinanceModel, FinanceViewModel>();
        CreateMap<FinanceViewModel, FinanceModel>();

        CreateMap<QuotationViewModel, FinanceModel>()
            .ForMember(dest => dest.Quotation, opt => opt.MapFrom(src => src));

        CreateMap<FinanceModel, QuotationViewModel>()
            .ForMember(dest => dest.AmountRequired, opt => opt.MapFrom(src => src.Quotation.AmountRequired))
            .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Quotation.Term))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.Title : null))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.FirstName : null))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.LastName : null))
            //.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.DateOfBirth : null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Quotation.Borrower.DateOfBirth))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.Mobile : null))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Quotation.Borrower != null ? src.Quotation.Borrower.Email : null))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
    }
}
