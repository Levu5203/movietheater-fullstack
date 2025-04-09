import { BehaviorSubject, of } from 'rxjs';
import { UserInformation } from '../src/models/auth/user-information.model';

// Mock Auth Service
export class MockAuthService {
  private readonly _isAuthenticated = new BehaviorSubject<boolean>(false);
  private readonly _userInformation = new BehaviorSubject<UserInformation | null>(null);

  login() {
    return of({
      accessToken: 'fake-token',
      user: { id: '1', username: 'testuser' },
    });
  }
  register() {
    return of({
      accessToken: 'fake-register-token',
      user: { id: '2', username: 'newuser' },
    });
  }
  logout() {
    this._isAuthenticated.next(false);
  }
  isAuthenticated() {
    return this._isAuthenticated.asObservable();
  }
  getUserInformation() {
    return this._userInformation.asObservable();
  }
  getUserInformationFromAccessToken() {
    return this._userInformation.asObservable();
  }
  getAccessToken() {
    return 'fake-token';
  }
}
