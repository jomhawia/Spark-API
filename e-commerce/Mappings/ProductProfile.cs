using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductGetDto>();

        CreateMap<ProductCreateDto, Product>()
            .ForMember(d => d.MainImage, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<ProductUpdateDto, Product>()
            .ForMember(d => d.MainImage, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}