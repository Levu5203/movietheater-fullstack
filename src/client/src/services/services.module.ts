import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileService } from './profile.service';
import {
  CUSTOMER_SERVICE,
  EMPLOYEE_SERVICE,
} from '../constants/injection.constant';
import { CustomerService } from './customer/customer.service';
import { EmployeeService } from './employee/employee.service';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [
    {
      provide: CUSTOMER_SERVICE,
      useClass: CustomerService,
    },
    {
      provide: EMPLOYEE_SERVICE,
      useClass: EmployeeService,
    },
    {
      provide: 'profileService',
      useClass: ProfileService,
    },
  ],
})
export class ServicesModule {}
