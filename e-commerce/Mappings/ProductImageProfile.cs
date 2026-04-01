using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class ProductImageProfile : Profile
{
    public ProductImageProfile()
    {
        CreateMap<ProductImage, ProductImageGetDto>();

        CreateMap<ProductImageCreateDto, ProductImage>()
            .ForMember(d => d.Image, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<ProductImageUpdateDto, ProductImage>()
            .ForMember(d => d.Image, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}