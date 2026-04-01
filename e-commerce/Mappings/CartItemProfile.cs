using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, CartItemGetDto>();

        CreateMap<CartItemCreateDto, CartItem>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<CartItemUpdateDto, CartItem>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}