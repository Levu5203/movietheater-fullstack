import { Observable } from 'rxjs';
import { LoginRequest } from '../../models/auth/login-request.model';
import { LoginResponse } from '../../models/auth/login-response.model';

export interface IAuthService {
  login(loginRequest: LoginRequest): Observable<LoginResponse>;
  isAuthenticated(): Observable<boolean>;
  logout(): void;
  getAccessToken(): string;
}
