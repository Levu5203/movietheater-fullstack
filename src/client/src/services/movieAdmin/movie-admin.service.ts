import { Injectable } from '@angular/core';
import { MasterDataService } from '../master-data/master-data.service';
import { MovieViewModel } from '../../models/movie/movie-view-model';
import { IMovieAdminServiceInterface } from './movie-admin.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MovieAdminService extends MasterDataService<MovieViewModel> implements IMovieAdminServiceInterface{

  constructor(protected override http: HttpClient) {
    super(http, 'Movie')
   }
}
