import { InjectionToken } from '@angular/core';
import { IAuthService } from '../services/auth/auth-service.interface';
import { IPermissionService } from '../services/permission/permission-service.interface';
import { ModalService } from '../services/modal.service';
import { ICustomerService } from '../services/customer/customer-service.interface';
import { IEmployeeService } from '../services/employee/employee-service.interface';
import { IRoomService } from '../services/room/room-service.interface';
import { IShowtimeServiceInterface } from '../services/showtime/showtime-service.interface';
import { IMovieServiceInterface } from '../services/movie/movie-service.interface';

export const AUTH_SERVICE = new InjectionToken<IAuthService>('AUTH_SERVICE');
export const MODAL_SERVICE = new InjectionToken<ModalService>('MODAL_SERVICE');
export const PERMISSION_SERVICE = new InjectionToken<IPermissionService>(
  'PERMISSION_SERVICE'
);
export const CUSTOMER_SERVICE = new InjectionToken<ICustomerService>(
  'CUSTOMER_SERVICE'
);
export const EMPLOYEE_SERVICE = new InjectionToken<IEmployeeService>(
  'EMPLOYEE_SERVICE'
);
export const ROOM_SERVICE = new InjectionToken<IRoomService>(
  'EMPLOYEE_SERVICE'
);
export const SHOWTIME_SERVICE = new InjectionToken<IShowtimeServiceInterface>('SHOWTIME_SERVICE');
export const MOVIE_SERVICE = new InjectionToken<IMovieServiceInterface>('MOVIE_SERVICE');
