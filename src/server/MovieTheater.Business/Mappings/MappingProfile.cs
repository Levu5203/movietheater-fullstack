using AutoMapper;
using MovieTheater.Business.Handlers.Seat;
using MovieTheater.Business.ViewModels.CinemaRoom;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Business.ViewModels.Ticket;
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

        CreateMap<ShowTime, CinemaViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CinemaRoom.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CinemaRoom.Name))
            .ForMember(dest => dest.SeatRow, opt => opt.MapFrom(src => src.CinemaRoom.SeatRows))
            .ForMember(dest => dest.SeatColumn, opt => opt.MapFrom(src => src.CinemaRoom.SeatColumns));
        CreateMap<CinemaRoom, CinemaViewModel>()
            .ForMember(dest => dest.SeatRow, opt => opt.MapFrom(src => src.SeatRows))
            .ForMember(dest => dest.SeatColumn, opt => opt.MapFrom(src => src.SeatColumns));

        CreateMap<Seat, SeatViewModel>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.CinemaRoomId))
            .ForMember(dest => dest.SeatType, opt => opt.MapFrom(src => src.SeatType))
            .ForMember(dest => dest.Row, opt => opt.MapFrom(src => src.Row))
            .ForMember(dest => dest.Column, opt => opt.MapFrom(src => src.Column))
            .ForMember(dest => dest.SeatStatus, opt => opt.MapFrom(src => src.seatStatus));
        CreateMap<SeatReverveCommand, Seat>();

        CreateMap<Ticket, TicketViewModel>()
            // .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat));
            // .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        // CreateMap<TicketViewModel, Ticket>()
        //     .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat));

        CreateMap<Invoice, InvoicePreviewViewModel>()
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.ShowTime.CinemaRoom.Name))
            .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.ShowTime.Movie.Name))
            .ForMember(dest => dest.ShowDate, opt => opt.MapFrom(src => src.ShowTime.ShowDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ShowTime.ShowTimeSlot.Time));

    }
}