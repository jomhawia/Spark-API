using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class SubCategoryProfile : Profile
{
    public SubCategoryProfile()
    {
        CreateMap<SubCategory, SubCategoryGetDto>();

        CreateMap<SubCategoryCreateDto, SubCategory>()
            .ForMember(d => d.Image, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<SubCategoryUpdateDto, SubCategory>()
            .ForMember(d => d.Image, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}