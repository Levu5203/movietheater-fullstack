import { Component, Inject } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { IAuthService } from '../../../services/auth/auth-service.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule, CommonModule],
  styleUrls: ['./forgotpassword.component.css'],
  templateUrl: './forgotpassword.component.html',
})
export class ForgotPasswordComponent {
  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  successMessage = '';
  errorMessage = '';

  constructor(@Inject(AUTH_SERVICE) private authService: IAuthService) {}

  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      const email = this.forgotPasswordForm.value.email!;
      this.authService.forgotPassword(email).subscribe({
        next: () => {
          this.successMessage = 'Check your email for reset link!';
          this.errorMessage = '';
        },
        error: (err) => {
          this.errorMessage = 'Error sending reset link';
          console.error(err);
        },
      });
    }
  }
}
