using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class ProductVariantProfile : Profile
{
    public ProductVariantProfile()
    {
        CreateMap<ProductVariant, ProductVariantGetDto>();

        CreateMap<ProductVariantCreateDto, ProductVariant>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<ProductVariantUpdateDto, ProductVariant>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}