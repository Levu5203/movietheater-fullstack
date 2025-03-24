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
import { faTimes } from '@fortawesome/free-solid-svg-icons';
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

  ngOnInit(): void {
    this.createForm();
  }

  public createForm() {
    this.form = new FormGroup({
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
          '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$'
        ),
      ]),
    });
  }

  public onSubmit(): void {
    this.authService.login(this.form.value).subscribe((response) => {
      if (response) {
        // Hide the modal
        this.closeModal();
      }
    });
  }
}
