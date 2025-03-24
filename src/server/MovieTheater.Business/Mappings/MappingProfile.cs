using System;
using AutoMapper;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.ViewModels.auth;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginRequestCommand, LoginResponse>();
        // Thêm các mapping khác tại đây
    }
}