using AutoMapper;
using QuoteCalculator.Domain.Models;
using QuoteCalculator.Domain.Models.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<QuotationDto, QuotationModel>();

        CreateMap<QuotationDto, BorrowerModel>();

        //CreateMap<QuotationDto, BorrowerModel>()
        //    .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Description));
    }
}
