import { Injectable } from '@angular/core';
import { MasterDataService } from '../master-data/master-data.service';
import { MovieviewModel } from '../../models/movie/movieview.model';
import { IMovieServiceInterface } from './movie-service.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MovieServiceService extends MasterDataService<MovieviewModel> implements IMovieServiceInterface{

  constructor(protected override http: HttpClient) {
    super(http, 'Movie')
   }

}
