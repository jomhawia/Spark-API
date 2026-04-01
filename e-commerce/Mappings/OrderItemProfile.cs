using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, OrderItemGetDto>();

        CreateMap<OrderItemCreateDto, OrderItem>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
            .ForMember(d => d.LineTotal, opt => opt.Ignore()); // محسوب بالسيرفس

        var updateMap = CreateMap<OrderItemUpdateDto, OrderItem>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
            .ForMember(d => d.LineTotal, opt => opt.Ignore()); // محسوب بالسيرفس

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}