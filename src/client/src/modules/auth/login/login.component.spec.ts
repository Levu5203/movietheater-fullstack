import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MockAuthService } from '../../../../testing/mock-service';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { RegisterComponent } from '../register/register.component';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let mockAuthService: MockAuthService;

  beforeEach(async () => {
    mockAuthService = new MockAuthService();

    await TestBed.configureTestingModule({
      imports: [
        LoginComponent,
        ReactiveFormsModule,
        CommonModule,
        FontAwesomeModule,
      ],
      providers: [{ provide: AUTH_SERVICE, useValue: mockAuthService }],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
