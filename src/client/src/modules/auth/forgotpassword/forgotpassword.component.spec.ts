import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotPasswordComponent } from './forgotpassword.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MockAuthService } from '../../../../testing/mock-service';
import { AUTH_SERVICE } from '../../../constants/injection.constant';

describe('ForgotpasswordComponent', () => {
  let component: ForgotPasswordComponent;
  let fixture: ComponentFixture<ForgotPasswordComponent>;

  let mockAuthService: MockAuthService;

  beforeEach(async () => {
    mockAuthService = new MockAuthService();

    await TestBed.configureTestingModule({
      imports: [
        ForgotPasswordComponent,
        ReactiveFormsModule,
        CommonModule,
        FontAwesomeModule,
      ],
      providers: [{ provide: AUTH_SERVICE, useValue: mockAuthService }],
    }).compileComponents();

    fixture = TestBed.createComponent(ForgotPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
