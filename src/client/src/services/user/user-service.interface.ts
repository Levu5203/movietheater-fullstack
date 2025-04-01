import { IMasterDataService } from '../master-data/master-data.service.interface';
import { UserModel } from '../../models/user/user.model';

export interface IUserService extends IMasterDataService<UserModel> {}
