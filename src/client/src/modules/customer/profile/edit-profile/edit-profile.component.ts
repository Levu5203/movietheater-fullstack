import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCalendar, IconDefinition } from '@fortawesome/free-solid-svg-icons';

import { ModalService } from '../../../../services/modal.service';
import { ProfileService } from '../../../../services/profile.service';
import { UserProfileViewModel } from '../../../../models/profile/user-profile.model';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, FontAwesomeModule, ReactiveFormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  public faCalendar: IconDefinition = faCalendar;
  
  currentUser!: UserProfileViewModel | null;
  form: FormGroup;
  
  public successMessage = '';
  error: string | null = null;
  public errorMessage: string = '';
  showErrorMessage: boolean = false;

  constructor(
    private readonly modalService: ModalService,
    private readonly fb: FormBuilder,
    private readonly profileService: ProfileService
  ) {
    this.form = this.fb.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.pattern('^[A-Za-z]+$'),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(50),
          Validators.pattern('^[A-Za-z]+$'),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(50),
          Validators.email,
        ],
      ],
      dateOfBirth: [null],
      address: ['', [Validators.maxLength(255)]],
      gender: ['Male', Validators.required],
      identityCard: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(18),
          Validators.pattern('^[0-9]{10,18}$'),
        ],
      ],
      phoneNumber: [null, Validators.pattern('/^[0-9]{10,15}$/')],
    });
    this.form.valueChanges.subscribe(() => (this.showErrorMessage = false));
    this.setupPhoneNumberValidation();
  }

  private setupPhoneNumberValidation() {
    const phoneControl = this.form.get('phoneNumber');

    phoneControl?.valueChanges.pipe(debounceTime(0)).subscribe((value) => {
      if (value && value.trim() !== '') {
        phoneControl.setValidators([Validators.pattern(/^\d{10,15}$/)]);
      } else {
        phoneControl.clearValidators();
      }
      phoneControl.updateValueAndValidity({ emitEvent: false });
    });
  }

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.profileService.getProfile().subscribe({
      next: (profile: UserProfileViewModel) => {
        this.currentUser = profile;
        this.form.patchValue({
          id: this.currentUser.id,
          username: this.currentUser.username,
          firstName: this.currentUser.firstName,
          lastName: this.currentUser.lastName,
          dateOfBirth: this.currentUser.dateOfBirth
            ? this.currentUser.dateOfBirth.toString().substring(0, 10)
            : '',
          gender: this.currentUser.gender,
          identityCard: this.currentUser.identityCard,
          email: this.currentUser.email,
          phoneNumber: this.currentUser.phoneNumber,
          address: this.currentUser.address ?? '',
        });
      },
      error: (error) => {
        this.error = 'Failed to load profile information.';
        console.error(error);
      },
    });
  }

  openModal() {
    this.modalService.open('changePassword');
  }

  onSubmit() {
    if (this.form.invalid) {
      console.log(this.form);

      this.showErrorMessage = true;
      this.errorMessage = 'All fields marked with an asterisk are required';
      return;
    }

    const updatedProfile: Partial<UserProfileViewModel> = {
      id: this.currentUser?.id,
      avatar: this.currentUser?.avatar,
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      dateOfBirth: this.form.value.dateOfBirth,
      gender: this.form.value.gender,
      identityCard: this.form.value.identityCard,
      email: this.form.value.email,
      phoneNumber: this.form.value.phoneNumber,
      address: this.form.value.address,
    };

    this.profileService.updateProfile(updatedProfile).subscribe({
      next: (profile) => {
        this.successMessage = 'Profile updated successfully!';
      },
      error: (error) => {
        this.showErrorMessage = true;
        this.errorMessage = error.message;
      },
    });
  }

  closeModal(){
    this.successMessage = '';
  }
}
