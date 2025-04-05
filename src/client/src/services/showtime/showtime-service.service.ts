import { Injectable } from '@angular/core';
import { ShowtimeviewModel } from '../../models/showtime/showtimeview.model';
import { IShowtimeServiceInterface } from './showtime-service.interface';
import { MasterDataService } from '../master-data/master-data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ShowtimeServiceService extends MasterDataService<ShowtimeviewModel> implements IShowtimeServiceInterface {

  constructor(protected override http: HttpClient) {
    super(http, 'Showtime/showtimes')
   }
}
