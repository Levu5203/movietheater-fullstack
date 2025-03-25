import { Injectable } from '@angular/core';
import { IAuthService } from './auth-service.interface';
import { LoginRequest } from '../../models/auth/login-request.model';
import { LoginResponse } from '../../models/auth/login-response.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { RegisterRequest } from '../../models/auth/register-request.model';
import { UserInformation } from '../../models/auth/user-information.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements IAuthService {
  private readonly apiUrl: string = 'http://localhost:5063/api/auth';
  private _isAuthenticated: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);

  private _isAuthenticated$: Observable<boolean> =
    this._isAuthenticated.asObservable();

  private _userInformation: BehaviorSubject<UserInformation | null> =
    new BehaviorSubject<UserInformation | null>(null);

  private _userInformation$: Observable<UserInformation | null> =
    this._userInformation.asObservable();

  constructor(private httpClient: HttpClient) {
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
    return localStorage.getItem('accessToken') || '';
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
    localStorage.removeItem('accessToken');
    localStorage.removeItem('userInformation');

    this._isAuthenticated.next(false);
    this._userInformation.next(null);
  }

  public getUserInformationFromAccessToken(): Observable<UserInformation | null> {
    // Using JWT to decode the access token and get the user information
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      const payload = JSON.parse(atob(accessToken.split('.')[1]));
      const userInformation: UserInformation = {
        id: payload.nameid,
        email: payload.email,
        displayName: payload.fullName,
        username: payload.unique_name,
        roles:
          payload[
            'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
          ],
      };
      console.log(userInformation);

      this._userInformation.next(userInformation);
    }
    return this._userInformation$;
  }
}
