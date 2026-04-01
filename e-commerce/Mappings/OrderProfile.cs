using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderGetDto>();

        CreateMap<OrderCreateDto, Order>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<OrderUpdateDto, Order>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore());

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}