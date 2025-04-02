import { UserModel } from '../../models/user/user.model';
import { IMasterDataService } from '../master-data/master-data.service.interface';

export interface IEmployeeService extends IMasterDataService<UserModel> {}
