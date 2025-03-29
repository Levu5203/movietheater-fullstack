using AutoMapper;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Thêm các mapping khác tại đây
        CreateMap<UserViewModel, User>().ReverseMap();

    }
}