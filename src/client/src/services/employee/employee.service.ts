import { Injectable } from '@angular/core';
import { MasterDataService } from '../master-data/master-data.service';
import { UserModel } from '../../models/user/user.model';
import { IEmployeeService } from './employee-service.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService
  extends MasterDataService<UserModel>
  implements IEmployeeService
{
  constructor(protected override http: HttpClient) {
    super(http, 'employees');
  }
}
