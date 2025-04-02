import { Injectable } from '@angular/core';
import { MasterDataService } from '../master-data/master-data.service';
import { IEmployeeService } from './employee-service.interface';
import { HttpClient } from '@angular/common/http';
import { EmployeeModel } from '../../models/user/employee.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService
  extends MasterDataService<EmployeeModel>
  implements IEmployeeService
{
  constructor(protected override http: HttpClient) {
    super(http, 'employees');
  }
}
