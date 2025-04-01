using AutoMapper;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<User, UserProfileViewModel>();
        // Thêm các mapping khác tại đây
        CreateMap<UserViewModel, User>().ReverseMap();

        CreateMap<CinemaRoom,CinemaRoomViewModel>();
        CreateMap<CinemaRoomViewModel,CinemaRoom>().ReverseMap();

    }
}