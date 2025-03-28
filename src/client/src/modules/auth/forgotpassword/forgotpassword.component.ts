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
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { ModalService } from '../../../services/modal.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule],
  styleUrls: ['./forgotpassword.component.css'],
  templateUrl: './forgotpassword.component.html',
})
export class ForgotPasswordComponent {
  public faTimes: IconDefinition = faTimes;

  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  successMessage = '';
  errorMessage = '';

  constructor(
    private readonly modalService: ModalService,
    @Inject(AUTH_SERVICE) private authService: IAuthService,
    private router: Router
  ) {}
  closeModal() {
    this.modalService.close();
  }
  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      const email = this.forgotPasswordForm.value.email!;
      this.authService.forgotPassword(email).subscribe({
        next: () => {
          this.successMessage = 'Check your email for reset link!';
          this.errorMessage = '';
          setTimeout(() => {
            this.closeModal();
            this.router.navigate(['/']);
          }, 1000);
        },
        error: (err) => {
          this.errorMessage = err.error?.message || 'Error sending reset link';
          console.error(err);
        },
      });
    }
  }
}
