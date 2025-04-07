import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICustomerService } from './customer-service.interface';
import { UserModel } from '../../models/user/user.model';
import { MasterDataService } from '../master-data/master-data.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerService
  extends MasterDataService<UserModel>
  implements ICustomerService
{
  constructor(protected override http: HttpClient) {
    super(http, 'customers');
  }
  updateStatus(userId: string): Observable<any> {
    return this.http.put<UserModel>(
      `${this.baseUrl}/update-status/${userId}`,
      {}
    );
  }
}
