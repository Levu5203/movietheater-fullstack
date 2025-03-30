using System;
using AutoMapper;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginRequestCommand, LoginResponse>();
        //
        CreateMap<Movie, MovieViewModel>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Type).ToList()))
            .ForMember(dest => dest.CinemaRooms,
                opt => opt.MapFrom(src => src.ShowTimes
                    .Select(st => st.CinemaRoom.Name)
                    .Distinct()
                    .ToList()))
            .ForMember(dest => dest.Showtimes,
                opt => opt.MapFrom(src => src.ShowTimes));
        //Mapper for showtime
        CreateMap<ShowTime, ShowtimeViewModel>()
            .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
            .ForMember(dest => dest.RoomName, 
                opt => opt.MapFrom(src => src.CinemaRoom.Name))
            .ForMember(dest => dest.StartTime, 
               opt => opt.MapFrom(src => src.ShowTimeSlot.Time.ToString(@"hh\:mm\:ss")));
    }
}