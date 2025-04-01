import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CinemaRoomViewModel } from '../../models/room/room.model';
import { MasterDataService } from '../master-data/master-data.service';
import { IRoomService } from './room-service.interface';

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
}
