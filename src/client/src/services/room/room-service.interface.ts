import { IMasterDataService } from '../master-data/master-data.service.interface';
import { CinemaRoomViewModel } from '../../models/room/room.model';
import { Observable } from 'rxjs';
import { SeatViewModel } from '../../models/seat/seat.model';

export interface IRoomService extends IMasterDataService<CinemaRoomViewModel> {
  loadRoomSeats(roomId: string): Observable<SeatViewModel[]>;

  loadRoomInfo(roomId: string): Observable<CinemaRoomViewModel>;

  updateSeats(roomId: string, seats: SeatViewModel[]): Observable<void>
}
