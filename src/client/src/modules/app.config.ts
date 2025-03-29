import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {
  AUTH_SERVICE,
  MODAL_SERVICE,
  PERMISSION_SERVICE,
} from '../constants/injection.constant';
import { AuthService } from '../services/auth/auth.service';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { ModalService } from '../services/modal.service';
import { PermissionService } from '../services/permission/permission.service';
import { authInterceptor } from '../interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor]), withFetch()),
    {
      provide: MODAL_SERVICE,
      useClass: ModalService,
    },
    {
      provide: AUTH_SERVICE,
      useClass: AuthService,
    },
    {
      provide: PERMISSION_SERVICE,
      useClass: PermissionService,
    },
  ],
};
