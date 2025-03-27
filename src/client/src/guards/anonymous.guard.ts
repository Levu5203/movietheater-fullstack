import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  RouterStateSnapshot,
} from '@angular/router';
import { PERMISSION_SERVICE } from '../constants/injection.constant';
import { inject } from '@angular/core';

export const anonymousGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const permissionService = inject(PERMISSION_SERVICE);
  return permissionService.isUnauthenticated();
};
