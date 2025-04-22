using MovieTheater.Business.ViewModels.Room;

namespace MovieTheater.Business.Handlers.CinemaRoom;

public class CinemaRoomSearchQuery: MasterDataSearchQuery<CinemaRoomViewModel>
{
    public int? MinCapacity{ get; set; }
    public int? MaxCapacity{ get; set; }
}
