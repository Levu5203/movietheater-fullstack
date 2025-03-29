import { Component, Inject } from '@angular/core';
import { ModalService } from '../../../../services/modal.service';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faCalendar,
  faEye,
  faEyeSlash,
  faTimes,
} from '@fortawesome/free-solid-svg-icons';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
  ValidationErrors,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProfileService } from '../../../../services/profile.service';
import { AuthService } from '../../../../services/auth/auth.service';
import { AUTH_SERVICE } from '../../../../constants/injection.constant';

@Component({
  selector: 'app-change-password',
  imports: [FontAwesomeModule, CommonModule, ReactiveFormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  public faTimes: IconDefinition = faTimes;
  public faCalendar: IconDefinition = faCalendar;

  public faEye: IconDefinition = faEye;
  public faEyeSlash: IconDefinition = faEyeSlash;

  public errorMessage: string = '';
  public showErrorMessage: boolean = false;
  public form!: FormGroup;
  public showPassword = false;
  public showConfirmPassword = false;
  public successMessage = '';

  public createForm() {
    this.form = new FormGroup(
      {
        password: new FormControl('', [Validators.required]),
        newPassword: new FormControl('', [
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
    const newPassword = control.get('newPassword');
    const confirmPassword = control.get('confirmPassword');

    if (!newPassword || !confirmPassword) {
      return null;
    }

    // Only validate if both fields have values
    if (newPassword.pristine || confirmPassword.pristine) {
      return null;
    }

    return newPassword.value === confirmPassword.value
      ? null
      : { passwordMismatch: true };
  }

  constructor(
    private readonly route: ActivatedRoute,
    private readonly profileService: ProfileService,
    @Inject(AUTH_SERVICE) private readonly authService: AuthService,
    private readonly modalService: ModalService
  ) {}

  ngOnInit() {
    this.createForm();
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.authService.getUserInformation().subscribe((userInfo) => {
      if (userInfo && userInfo.id) {
        const command = {
          userId: userInfo.id, // Lấy userId từ kết quả của Observable
          currentPassword: this.form.get('password')?.value,
          newPassword: this.form.get('newPassword')?.value,
        };

        this.profileService.changePassword(command).subscribe({
          next: (response) => {
            this.successMessage = 'Password changed successfully!';
            this.showErrorMessage = false;
            this.errorMessage = '';
            this.form.reset();
          },
          error: (error) => {
            this.showErrorMessage = true;
            this.errorMessage =
              error.error || 'An error occurred. Please try again.';
          },
        });
      } else {
        this.showErrorMessage = true;
        this.errorMessage =
          'Unable to retrieve user information. Please try again.';
      }
    });
  }

  closeModal() {
    this.modalService.close();
  }
}
