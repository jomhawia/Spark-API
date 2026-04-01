using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressGetDto>();

        CreateMap<AddressCreateDto, Address>();

        CreateMap<AddressUpdateDto, Address>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}