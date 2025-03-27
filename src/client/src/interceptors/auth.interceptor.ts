import {
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { PERMISSION_SERVICE } from '../constants/injection.constant';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const permissionsService = inject(PERMISSION_SERVICE);
  const accessToken = permissionsService.getAccessToken();

  if (accessToken) {
    console.log('Access token');

    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`,
      },
    });

    return next(req);
  }
  return next(req);
};
