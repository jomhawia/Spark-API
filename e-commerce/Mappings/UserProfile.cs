using AutoMapper;
using e_commerce.Entites;
using e_commerce.Services.DTO;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserGetDto>();

        CreateMap<UserCreateDto, User>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        var updateMap = CreateMap<UserUpdateDto, User>()
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        // ignore nulls في partial update
        updateMap.ForAllMembers(opt =>
            opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}