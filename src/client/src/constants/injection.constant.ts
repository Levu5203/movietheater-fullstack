import { InjectionToken } from '@angular/core';
import { IAuthService } from '../services/auth/auth-service.interface';
import { IPermissionService } from '../services/permission/permission-service.interface';
import { ModalService } from '../services/modal.service';
import { ICustomerService } from '../services/customer/customer-service.interface';

export const AUTH_SERVICE = new InjectionToken<IAuthService>('AUTH_SERVICE');
export const MODAL_SERVICE = new InjectionToken<ModalService>('MODAL_SERVICE');
export const PERMISSION_SERVICE = new InjectionToken<IPermissionService>(
  'PERMISSION_SERVICE'
);
export const CUSTOMER_SERVICE = new InjectionToken<ICustomerService>(
  'CUSTOMER_SERVICE'
);
