import { InjectionToken } from '@angular/core';
import { IAuthService } from '../services/auth/auth-service.interface';
import { IPermissionService } from '../services/permission/permission-service.interface';
import { ModalService } from '../services/modal.service';
import { IUserService } from '../services/user/user-service.interface';
import { IPromotionService } from '../services/promotion/promotion-service.interface';

export const AUTH_SERVICE = new InjectionToken<IAuthService>('AUTH_SERVICE');
export const MODAL_SERVICE = new InjectionToken<ModalService>('MODAL_SERVICE');
export const PERMISSION_SERVICE = new InjectionToken<IPermissionService>(
  'PERMISSION_SERVICE'
);
export const USER_SERVICE = new InjectionToken<IUserService>('USER_SERVICE');
export const PROMOTION_SERVICE = new InjectionToken<IPromotionService>('PROMOTION_SERVICE');
