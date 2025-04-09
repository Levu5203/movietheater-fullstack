using AutoMapper;
using MovieTheater.Business.Handlers.Seat;
using MovieTheater.Business.ViewModels.CinemaRoom;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Business.ViewModels.Ticket;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Models.Common;
using MovieTheater.Business.ViewModels.Promotion;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserProfileViewModel>();
        // Thêm các mapping khác tại đây
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<EmployeeViewModel, User>().ReverseMap();

        //
        CreateMap<Movie, MovieViewModel>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleasedDate))
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
            .ForMember(dest => dest.RoomId,
                opt => opt.MapFrom(src => src.CinemaRoom.Id))
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
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.CinemaRoom.Name))
            .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
            .ForMember(dest => dest.ShowDate, opt => opt.MapFrom(src => src.ShowTime.ShowDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ShowTime.ShowTimeSlot.Time.ToString(@"hh\:mm")))
            .ReverseMap();

        CreateMap<Invoice, InvoicePreviewViewModel>()
            .ForMember(dest => dest.TotalMoney, opt => opt.MapFrom(src => src.TotalMoney))
            .ForMember(dest => dest.AddedScore, opt => opt.MapFrom(src => src.AddedScore))
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.ShowTime.CinemaRoom.Name))
            .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.ShowTime.Movie.Name))
            .ForMember(dest => dest.ShowDate, opt => opt.MapFrom(src => src.ShowTime.ShowDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ShowTime.ShowTimeSlot.Time.ToString(@"hh\:mm\:ss")));

        CreateMap<Invoice, InvoiceViewModel>()
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.UserPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.CinemaRoom.Name))
            .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Name))
            .ForMember(dest => dest.ShowDate, opt => opt.MapFrom(src => src.ShowTime.ShowDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ShowTime.ShowTimeSlot.Time.ToString(@"hh\:mm")))
            .ReverseMap();

        CreateMap<Promotion, PromotionViewModel>();
        CreateMap<CinemaRoom,CinemaRoomViewModel>();
        CreateMap<CinemaRoomViewModel,CinemaRoom>().ReverseMap();

        CreateMap<Seat, SeatViewModel>();
        CreateMap<SeatViewModel, Seat>().ReverseMap();
    }
}