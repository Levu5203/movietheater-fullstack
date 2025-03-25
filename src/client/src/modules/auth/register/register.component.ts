import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { ModalService } from '../../../services/modal.service';
import {
  faCalendar,
  faEye,
  faEyeSlash,
  faTimes,
} from '@fortawesome/free-solid-svg-icons';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { IAuthService } from '../../../services/auth/auth-service.interface';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  public faTimes: IconDefinition = faTimes;
  public faCalendar: IconDefinition = faCalendar;

  public faEye: IconDefinition = faEye;
  public faEyeSlash: IconDefinition = faEyeSlash;

  public errorMessage: string = '';
  public showErrorMessage: boolean = false;
  constructor(
    private readonly modalService: ModalService,
    @Inject(AUTH_SERVICE) private readonly authService: IAuthService
  ) {}
  openModal() {
    this.modalService.open('login');
  }

  closeModal() {
    this.modalService.close();
  }

  public form!: FormGroup;
  public showPassword = false;
  public showConfirmPassword = false;

  ngOnInit(): void {
    this.createForm();
  }

  public createForm() {
    this.form = new FormGroup(
      {
        firstName: new FormControl('', [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.pattern('^[A-Za-z]+$'),
        ]),
        lastName: new FormControl('', [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.pattern('^[A-Za-z]+$'),
        ]),
        username: new FormControl('', [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(50),
        ]),
        dateOfBirth: new FormControl(null),
        gender: new FormControl('Male', [Validators.required]),
        email: new FormControl('', [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(50),
          Validators.email,
        ]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(20),
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/
          ),
        ]),
        confirmPassword: new FormControl('', [Validators.required]),
        identityCard: new FormControl('', [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(18),
          Validators.pattern('^[0-9]{10,18}$'),
        ]),
        phoneNumber: new FormControl(null, [
          Validators.pattern('/^[0-9]{10,15}$/'),
        ]),
      },
      { validators: this.passwordMatchValidator }
    );
    // this.form.get('phoneNumber')?.valueChanges.subscribe((value) => {
    //   const control = this.form.get('phoneNumber');
    //   if (value && value.trim() !== '') {
    //     control?.setValidators([Validators.pattern('^[0-9]{10,15}$')]);
    //   } else {
    //     control?.clearValidators();
    //   }
    //   control?.updateValueAndValidity();
    // });

    this.form.valueChanges.subscribe(() => {
      this.showErrorMessage = false;
    });
    this.setupPhoneNumberValidation();
  }

  private setupPhoneNumberValidation() {
    const phoneControl = this.form.get('phoneNumber');

    phoneControl?.valueChanges
      .pipe(
        // Add a small delay to avoid ExpressionChangedAfterItHasBeenCheckedError
        debounceTime(0)
      )
      .subscribe((value) => {
        if (value && value.trim() !== '') {
          phoneControl.setValidators([
            Validators.pattern(/^[0-9]{10,15}$/), // Correct regex pattern
            // Add any other validators you want when field has value
          ]);
        } else {
          phoneControl.clearValidators();
        }
        phoneControl.updateValueAndValidity({ emitEvent: false });
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

  public onSubmit(): void {
    if (this.form.invalid) {
      console.log(this.form);

      this.showErrorMessage = true;
      this.errorMessage = 'All fields marked with an asterisk are required';
      return;
    }
    this.authService.register(this.form.value).subscribe({
      next: (response) => {
        if (response) {
          // Hide the modal
          this.closeModal();
        }
      },

      error: (error) => {
        this.showErrorMessage = true;
        this.errorMessage = error.message;
      },
    });
  }
}
