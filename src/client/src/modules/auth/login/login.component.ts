import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { ModalService } from '../../../services/modal.service';
import { faTimes, faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AUTH_SERVICE } from '../../../constants/injection.constant';
import { IAuthService } from '../../../services/auth/auth-service.interface';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  public faTimes: IconDefinition = faTimes;
  public faEye: IconDefinition = faEye;
  public faEyeSlash: IconDefinition = faEyeSlash;

  public errorMessage: string = '';
  public showErrorMessage: boolean = false;
  constructor(
    private modalService: ModalService,
    @Inject(AUTH_SERVICE) private authService: IAuthService
  ) {
    this.authService.isAuthenticated().subscribe((res) => {
      if (res) {
        this.closeModal();
      }
    });
  }
  openModal() {
    this.modalService.open('register');
  }

  closeModal() {
    this.modalService.close();
  }

  public form!: FormGroup;

  public showPassword = false;
  public showConfirmPassword = false;

  togglePasswordVisibility(field: 'password' | 'confirmPassword') {
    if (field === 'password') {
      this.showPassword = !this.showPassword;
    } else {
      this.showConfirmPassword = !this.showConfirmPassword;
    }
  }

  ngOnInit(): void {
    this.createForm();
  }

  public createForm() {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
      ]),
    });

    this.form.valueChanges.subscribe(() => {
      this.showErrorMessage = false;
    });
  }

  public onSubmit(): void {
    if (this.form.invalid) {
      this.errorMessage = 'Email and password are required';
      return;
    }

    this.authService.login(this.form.value).subscribe({
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
