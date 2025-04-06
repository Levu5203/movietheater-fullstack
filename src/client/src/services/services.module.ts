import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileService } from './profile.service';
import {
  CUSTOMER_SERVICE,
  EMPLOYEE_SERVICE,
  MOVIE_SERVICE,
  ROOM_SERVICE,
  SHOWTIME_SERVICE,
} from '../constants/injection.constant';
import { CustomerService } from './customer/customer.service';
import { EmployeeService } from './employee/employee.service';
import { RoomService } from './room/room.service';
import { ShowtimeServiceService } from './showtime/showtime-service.service';
import { MovieServiceService } from './movie/movie-service.service';

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
    {
      provide: ROOM_SERVICE,
      useClass: RoomService,
    },
    {
      provide: SHOWTIME_SERVICE,
      useClass: ShowtimeServiceService,
    },
    {
      provide: MOVIE_SERVICE,
      useClass: MovieServiceService,
    }
  ],
})
export class ServicesModule {}
