import { IMasterDataService } from '../master-data/master-data.service.interface';
import { UserModel } from '../../models/user/user.model';
import { Observable } from 'rxjs';

export interface ICustomerService extends IMasterDataService<UserModel> {
  updateStatus(userId: string): Observable<any>;
}
