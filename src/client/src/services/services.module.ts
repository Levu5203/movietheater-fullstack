import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileService } from './profile/profile.service';
import {
  BOOKING_SERVICE,
  CUSTOMER_SERVICE,
  EMPLOYEE_SERVICE,
  MOVIE_ADMIN_SERVICE,
  MOVIE_SERVICE,
  PROFILE_SERVICE,
  ROOM_SERVICE,
  SHOWTIME_SERVICE,
} from '../constants/injection.constant';
import { CustomerService } from './customer/customer.service';
import { EmployeeService } from './employee/employee.service';
import { RoomService } from './room/room.service';
import { ShowtimeServiceService } from './showtime/showtime-service.service';
import { MovieServiceService } from './movie/movie-service.service';
import { BookingService } from './booking/booking.service';
import { MovieAdminService } from './movieAdmin/movie-admin.service';

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
      provide: PROFILE_SERVICE,
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
    },
    {
      provide: BOOKING_SERVICE,
      useClass: BookingService,
    },
    {
      provide: MOVIE_ADMIN_SERVICE,
      useClass: MovieAdminService,
    }
  ],
})
export class ServicesModule {}
