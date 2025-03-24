import { Observable } from 'rxjs';
import { LoginRequest } from '../../models/auth/login-request.model';
import { LoginResponse } from '../../models/auth/login-response.model';
import { RegisterRequest } from '../../models/auth/register-request.model';
import { UserInformation } from '../../models/auth/user-information.model';

export interface IAuthService {
  login(loginRequest: LoginRequest): Observable<LoginResponse>;
  register(registerRequest: RegisterRequest): Observable<LoginResponse>;
  isAuthenticated(): Observable<boolean>;
  logout(): void;
  getAccessToken(): string;
  getUserInformation(): Observable<UserInformation | null>;
  getUserInformationFromAccessToken(): Observable<UserInformation | null>;
}
