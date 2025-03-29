using AutoMapper;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginRequestCommand, LoginResponse>();
        CreateMap<User, UserProfileViewModel>();
        // Thêm các mapping khác tại đây
    }
}