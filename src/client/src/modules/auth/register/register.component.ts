import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
import { faCalendar, faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule, FontAwesomeModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  public faTimes: IconDefinition = faTimes;

  public faCalendar: IconDefinition = faCalendar;


  constructor(private modalService: ModalService) {}
  openModal() {
    this.modalService.open('login');
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
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(255),
      ]),
      dateOfBirth: new FormControl(new Date()),
      username: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(255),
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/
        ),
      ]),
      confirmPassword: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
        Validators.pattern(this.form?.get('password')?.value),
      ]),
      identityCard: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(255),
      ]),
      address: new FormControl(''),
      phoneNumber: new FormControl(''),
    });
  }
}
