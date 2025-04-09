import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetPasswordComponent } from './resetpassword.component';
import { MockAuthService } from '../../../../testing/mock-service';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

describe('ResetPasswordComponent', () => {
  let component: ResetPasswordComponent;
  let fixture: ComponentFixture<ResetPasswordComponent>;
  let mockAuthService: MockAuthService;

  beforeEach(async () => {
    mockAuthService = new MockAuthService();

    await TestBed.configureTestingModule({
      imports: [
        ResetPasswordComponent,
        ReactiveFormsModule,
        CommonModule,
        FontAwesomeModule,
      ],
      providers: [{ provide: AUTH_SERVICE, useValue: mockAuthService }],
    }).compileComponents();

    fixture = TestBed.createComponent(ResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
