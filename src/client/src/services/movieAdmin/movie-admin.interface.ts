import { MovieViewModel } from "../../models/movie/movie-view-model";
import { IMasterDataService } from "../master-data/master-data.service.interface";

export interface IMovieAdminServiceInterface extends IMasterDataService<MovieViewModel>{
}