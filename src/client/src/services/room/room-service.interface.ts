import { IMasterDataService } from '../master-data/master-data.service.interface';
import { CinemaRoomViewModel } from '../../models/room/room.model';

export interface IRoomService extends IMasterDataService<CinemaRoomViewModel> {}
