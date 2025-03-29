import { Component, Inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { IAuthService } from '../../../services/auth/auth-service.interface';
import { CommonModule } from '@angular/common';
import {
  IconDefinition,
  FontAwesomeModule,
} from '@fortawesome/angular-fontawesome';
import {
  faCalendar,
  faEye,
  faEyeSlash,
  faTimes,
} from '@fortawesome/free-solid-svg-icons';
import { ModalService } from '../../../services/modal.service';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './resetpassword.component.html',
})
export class ResetPasswordComponent implements OnInit {
  public faTimes: IconDefinition = faTimes;
  public faCalendar: IconDefinition = faCalendar;

  public faEye: IconDefinition = faEye;
  public faEyeSlash: IconDefinition = faEyeSlash;

  public errorMessage: string = '';
  public showErrorMessage: boolean = false;
  public form!: FormGroup;
  public showPassword = false;
  public showConfirmPassword = false;
  public email = '';
  public token = '';
  public successMessage = '';

  public createForm() {
    this.form = new FormGroup(
      {
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20),
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/
          ),
        ]),
        confirmPassword: new FormControl('', [Validators.required]),
      },
      { validators: this.passwordMatchValidator }
    );

    this.form.valueChanges.subscribe(() => {
      this.showErrorMessage = false;
    });
  }
  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (!password || !confirmPassword) {
      return null;
    }

    // Only validate if both fields have values
    if (password.pristine || confirmPassword.pristine) {
      return null;
    }

    return password.value === confirmPassword.value
      ? null
      : { passwordMismatch: true };
  }

  constructor(
    private readonly route: ActivatedRoute,
    @Inject(AUTH_SERVICE) private readonly authService: IAuthService,
    private readonly modalService: ModalService,
    private readonly router: Router
  ) {}
  openModal() {
    this.modalService.open('forgot-password');
  }
  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.email = params['email'];
      this.token = decodeURIComponent(params['token']).replace(/ /g, '+');
    });
    this.createForm();
  }

  onSubmit() {
    if (this.form.invalid) {
      this.showErrorMessage = true;
      this.errorMessage = 'All fields marked with an asterisk are required';
      return;
    }
    const password = this.form.get('password')?.value;

    this.authService.resetPassword(this.token, password, this.email).subscribe({
      next: (response) => {
        this.successMessage = response.message;
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.showErrorMessage = true;
        this.errorMessage = err.error?.message || 'Error resetting password';
        console.error('Reset password error:', err);
      },
    });
  }
}
