import {
  Component,
  ElementRef,
  Inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  faCalendar,
  faCamera,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';

import { ModalService } from '../../../../services/modal.service';
import { ProfileService } from '../../../../services/profile/profile.service';
import { UserProfileViewModel } from '../../../../models/profile/user-profile.model';
import { debounceTime } from 'rxjs';
import { IProfileService } from '../../../../services/profile/profile-service.interface';
import { PROFILE_SERVICE } from '../../../../constants/injection.constant';
import { ServicesModule } from '../../../../services/services.module';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    ServicesModule,
  ],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  public faCalendar: IconDefinition = faCalendar;
  public faCamera: IconDefinition = faCamera;

  public currentUser!: UserProfileViewModel | null;
  public form!: FormGroup;

  public successMessage = '';
  public error: string | null = null;
  public errorMessage: string = '';
  public showErrorMessage: boolean = false;

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;
  initialAvatarUrl: string | null = null;
  previewUrl: string | ArrayBuffer | null = null;
  selectedFile: File | null = null;
  avatarChanged = false;

  avatarToDisplay(): string | ArrayBuffer | null {
    return this.previewUrl || this.initialAvatarUrl;
  }

  triggerFileInput() {
    if (this.fileInput) {
      this.fileInput.nativeElement.click();
    } else {
      console.warn('fileInput is not defined');
    }
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.selectedFile = file;

      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl = reader.result;
      };
      reader.readAsDataURL(file);
      this.avatarChanged = true;
    }
  }

  constructor(
    private readonly modalService: ModalService,
    private readonly fb: FormBuilder,
    @Inject(PROFILE_SERVICE) private readonly profileService: IProfileService
  ) {}

  ngOnInit(): void {
    this.createForm();
    this.loadUserProfile();
  }

  public createForm() {
    this.form = new FormGroup({
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(50),
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      email: new FormControl(),
      username: new FormControl(),
      dateOfBirth: new FormControl(null),
      address: new FormControl(''),
      gender: new FormControl('Male', [Validators.required]),
      identityCard: new FormControl('', [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(18),
        Validators.pattern('^[0-9]{10,18}$'),
      ]),
      phoneNumber: new FormControl(null, [
        Validators.pattern(/^[0-9]{10,15}$/),
      ]),
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
        this.form.get('username')?.disable(); // Disable the 'username' field
        this.form.get('email')?.disable(); // Disable the 'email' field
        this.initialAvatarUrl =
          typeof profile.avatar === 'string' ? profile.avatar : null;
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
      this.showErrorMessage = true;
      this.errorMessage = 'All fields marked with an asterisk are required';
      return;
    }

    if (!this.currentUser) {
      this.showErrorMessage = true;
      this.errorMessage = 'Please log in to continue';
      return;
    }

    // const updatedProfile: Partial<UserProfileViewModel> = {
    //   id: this.currentUser?.id,
    //   avatar: this.currentUser?.avatar,
    //   firstName: this.form.value.firstName,
    //   lastName: this.form.value.lastName,
    //   dateOfBirth: this.form.value.dateOfBirth,
    //   gender: this.form.value.gender,
    //   identityCard: this.form.value.identityCard,
    //   phoneNumber: this.form.value.phoneNumber,
    //   address: this.form.value.address,
    // };

    const data: UserProfileViewModel = this.form.getRawValue();

    const formData = new FormData();
    formData.append('id', this.currentUser.id);
    formData.append('firstName', data.firstName);
    formData.append('lastName', data.lastName);
    formData.append('email', data.email);
    formData.append('username', data.username);
    formData.append('gender', data.gender);
    formData.append('identityCard', data.identityCard);
    formData.append('address', data.address ?? '');
    formData.append('phoneNumber', data.phoneNumber ?? '');
    formData.append(
      'dateOfBirth',
      data.dateOfBirth ? data.dateOfBirth.toLocaleString() : ''
    );

    if (this.selectedFile) {
      console.log('update with file selected');

      formData.append('avatar', this.selectedFile);
    }

    this.profileService.updateProfileWithFile(formData).subscribe({
      next: () => {
        this.successMessage = 'Profile updated successfully!';
        this.showErrorMessage = false;
      },
      error: (error) => {
        this.showErrorMessage = true;
        this.errorMessage = error.message;
      },
    });
  }

  closeModal() {
    this.successMessage = '';
  }
}
