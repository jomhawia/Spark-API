using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandGetDto>();

        CreateMap<BrandCreateDto, Brand>()
            .ForMember(d => d.Logo, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<BrandUpdateDto, Brand>()
            .ForMember(d => d.Logo, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}