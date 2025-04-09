import { AuthService } from './auth.service';
import { TestBed } from '@angular/core/testing';

describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [],
      providers: [AuthService],
    });
    service = TestBed.inject(AuthService);
  });

  it('should create an instance', () => {
    expect(service).toBeTruthy();
  });
});
