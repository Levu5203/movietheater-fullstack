import { Component, Inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { IAuthService } from '../../../services/auth/auth-service.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './resetpassword.component.html',
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm = new FormGroup({
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    confirmPassword: new FormControl('', [Validators.required]),
  });

  email = '';
  token = '';
  successMessage = '';
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    @Inject(AUTH_SERVICE) private authService: IAuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.email = params['email'];
      this.token = params['token'];
    });
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const password = this.resetPasswordForm.value.password!;
      const confirmPassword = this.resetPasswordForm.value.confirmPassword!;
      if (password !== confirmPassword) {
        this.errorMessage = 'Passwords do not match!';
        return;
      }

      this.authService
        .resetPassword(this.email, this.token, password)
        .subscribe({
          next: () => {
            this.successMessage = 'Password reset successfully!';
            this.errorMessage = '';
            setTimeout(() => this.router.navigate(['/']), 2000);
          },
          error: (err) => {
            this.errorMessage = 'Error resetting password';
            console.error(err);
          },
        });
    }
  }
}
