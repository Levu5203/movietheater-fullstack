import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { ModalService } from '../../../services/modal.service';
import { faTimes, faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import {
  AUTH_SERVICE,
  MODAL_SERVICE,
} from '../../../constants/injection.constant';
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
    private readonly modalService: ModalService,
    @Inject(AUTH_SERVICE) private readonly authService: IAuthService,
    private readonly router: Router
  ) {}
  openModal(modalName: string) {
    this.modalService.open(modalName);
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
      this.showErrorMessage = true;
      this.errorMessage = 'Email and password are required';
      return;
    }

    this.authService.login(this.form.value).subscribe({
      next: (response) => {
        if (response) {
          const userRoles =
            this.authService.getUserInformationFromAccessToken()?.roles;
          console.log(userRoles);

          if (userRoles && this.authService.hasAnyRole(['Admin', 'Employee'])) {
            this.router.navigate(['/admin']);
            this.closeModal();
          } else {
            this.closeModal();
          }
        }
      },

      error: (error) => {
        this.showErrorMessage = true;
        this.errorMessage = error.message;
      },
    });
  }
}
