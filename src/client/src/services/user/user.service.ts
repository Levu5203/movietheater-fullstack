import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserService } from './user-service.interface';
import { UserModel } from '../../models/user/user.model';
import { MasterDataService } from '../master-data/master-data.service';

@Injectable({
  providedIn: 'root',
})
export class UserService
  extends MasterDataService<UserModel>
  implements IUserService
{
  constructor(protected override http: HttpClient) {
    super(http, 'user/customers');
  }
}
