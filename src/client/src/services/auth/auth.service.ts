import { Injectable } from '@angular/core';
import { IAuthService } from './auth-service.interface';
import { LoginRequest } from '../../models/auth/login-request.model';
import { LoginResponse } from '../../models/auth/login-response.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { RegisterRequest } from '../../models/auth/register-request.model';
import { UserInformation } from '../../models/auth/user-information.model';
import { UserInformationFromToken } from '../../models/auth/user-information-from-token';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private readonly apiUrl: string = 'http://localhost:5063/api/auth';
  private readonly _isAuthenticated: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);

  private readonly _isAuthenticated$: Observable<boolean> =
    this._isAuthenticated.asObservable();

  private readonly _userInformation: BehaviorSubject<UserInformation | null> =
    new BehaviorSubject<UserInformation | null>(null);

  private readonly _userInformation$: Observable<UserInformation | null> =
    this._userInformation.asObservable();

  constructor(private readonly httpClient: HttpClient) {
    // Check if the access token is present in local storage
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      this._isAuthenticated.next(true);
    }

    const userInformation = localStorage.getItem('userInformation');
    if (userInformation) {
      this._userInformation.next(JSON.parse(userInformation));
    }
  }
  getAccessToken(): string {
    return localStorage.getItem('accessToken') ?? '';
  }

  public isAuthenticated(): Observable<boolean> {
    return this._isAuthenticated$;
  }

  public getUserInformation(): Observable<UserInformation | null> {
    return this._userInformation$;
  }

  public login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.httpClient
      .post<LoginResponse>(this.apiUrl + '/login', loginRequest)
      .pipe(
        tap((response: LoginResponse) => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem(
            'userInformation',
            JSON.stringify(response.user)
          );
          this._isAuthenticated.next(true);
          this._userInformation.next(response.user);
          this.checkTokenExpired();
        }),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'An error occurred. Please try again.';

          if (error.status === 401) {
            errorMessage = error.error?.message || 'Invalid email or password.';
          } else if (error.status === 400) {
            errorMessage = error.error?.message || 'Invalid request data.';
          } else if (error.status === 0) {
            errorMessage =
              'Unable to connect to the server. Please check your internet connection.';
          }

          return throwError(() => new Error(errorMessage));
        })
      );
  }

  public register(registerRequest: RegisterRequest): Observable<LoginResponse> {
    return this.httpClient
      .post<LoginResponse>(this.apiUrl + '/register', registerRequest)
      .pipe(
        tap((response: LoginResponse) => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem(
            'userInformation',
            JSON.stringify(response.user)
          );
          this._isAuthenticated.next(true);
          this._userInformation.next(response.user);
          this.checkTokenExpired();
        }),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'An error occurred. Please try again.';

          if (error.status === 400) {
            errorMessage = error.error?.message || 'Invalid request data.';
          } else if (error.status === 0) {
            errorMessage =
              'Unable to connect to the server. Please check your internet connection.';
          }

          return throwError(() => new Error(errorMessage));
        })
      );
  }

  logout(): void {
    // Remove the access token from local storage
    localStorage.clear();

    this._isAuthenticated.next(false);
    this._userInformation.next(null);
  }

  public getUserInformationFromAccessToken(): UserInformationFromToken | null {
    // Using JWT to decode the access token and get the user information
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      const payload = JSON.parse(atob(accessToken.split('.')[1]));
      const userInformation: UserInformationFromToken = {
        id: payload.nameid,
        email: payload.email,
        displayName: payload.fullName,
        username: payload.unique_name,
        roles:
          payload[
            'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
          ],
        exp: payload.exp,
      };
      return userInformation;
    }
    return null;
  }
  public isTokenExpired(): boolean {
    try {
      const userInformationFromToken = this.getUserInformationFromAccessToken();
      const expiry = userInformationFromToken!.exp;

      return Math.floor(new Date().getTime() / 1000) >= expiry;
    } catch {
      return true;
    }
  }

  private checkTokenExpired(): void {
    setInterval(() => {
      if (this.isTokenExpired()) {
        this.logout();
      }
    }, 10000);
  }
}
