namespace e_commerce.Mappings
{
    using AutoMapper;
    using e_commerce.Entites;
    using e_commerce.Services.DTO;

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>();

            CreateMap<CategoryCreateDto, Category>()
                .ForMember(d => d.Image, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

            var updateMap = CreateMap<CategoryUpdateDto, Category>()
                .ForMember(d => d.Image, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

            updateMap.ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
