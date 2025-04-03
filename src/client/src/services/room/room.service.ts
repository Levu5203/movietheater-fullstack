import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CinemaRoomViewModel } from '../../models/room/room.model';
import { IRoomService } from './room-service.interface';
import { catchError, Observable } from 'rxjs';
import { SeatViewModel } from '../../models/seat/seat.model';
import { MasterDataService } from '../master-data/master-data.service';

@Injectable({
  providedIn: 'root',
})
export class RoomService
  extends MasterDataService<CinemaRoomViewModel>
  implements IRoomService
{
  constructor(protected override http: HttpClient) {
    super(http, 'rooms');
  }

  loadRoomSeats(roomId: string): Observable<SeatViewModel[]> {
    return this.http.get<SeatViewModel[]>(`${this.baseUrl}/${roomId}/seats`);
  }

  loadRoomInfo(roomId: string): Observable<CinemaRoomViewModel> {
    return this.http.get<CinemaRoomViewModel>(`${this.baseUrl}/${roomId}`);
  }

  updateSeats(roomId: string, seats: SeatViewModel[]): Observable<any> {
    const payload = {
      commands: seats.map((seat) => ({
        id: seat.id,
        NewType: seat.seatType,
      })),
    };
    console.log(payload);
    
    return this.http
      .put<any>(`${this.baseUrl}/${roomId}/update-seats`, payload)
      .pipe(
        catchError((error) => {
          console.error('Failed to update seats:', error);
          throw error;
        })
      );
  }
}
