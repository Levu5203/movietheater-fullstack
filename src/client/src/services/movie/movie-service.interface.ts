import { MovieviewModel } from "../../models/movie/movieview.model";
import { IMasterDataService } from "../master-data/master-data.service.interface";

export interface IMovieServiceInterface extends IMasterDataService<MovieviewModel>{
}
