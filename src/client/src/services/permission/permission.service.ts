import { Inject, Injectable } from '@angular/core';
import { AUTH_SERVICE } from '../../constants/injection.constant';
import { IAuthService } from '../auth/auth-service.interface';
import { Router } from '@angular/router';
import { IPermissionService } from './permission-service.interface';

@Injectable({
  providedIn: 'root',
})
export class PermissionService implements IPermissionService {
  private isLoggedIn: boolean = false;
  constructor(
    @Inject(AUTH_SERVICE) private readonly authService: IAuthService,
    private readonly router: Router
  ) {
    this.authService.isAuthenticated().subscribe((isAuthenticated) => {
      this.isLoggedIn = isAuthenticated;
    });
  }

  getAccessToken(): string {
    return this.authService.getAccessToken();
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }

  isAdmin(): boolean {
    if (!this.isAuthenticated()) return false;

    const userInfo = this.authService.getUserInformationFromAccessToken();
    return userInfo?.roles?.includes('Admin') ?? false;
  }

  isUnauthenticated(): boolean {
    if (this.isAuthenticated()) {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }

  checkAdminOrEmployeePermission(): boolean {
    if (
      !this.isAuthenticated() ||
      !this.authService.hasAnyRole(['Admin', 'Employee'])
    ) {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
