import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faCamera } from '@fortawesome/free-solid-svg-icons';
import { CommonModule } from '@angular/common';
import { EmployeeModel } from '../../../../models/user/employee.model';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { EMPLOYEE_SERVICE } from '../../../../constants/injection.constant';
import { IEmployeeService } from '../../../../services/employee/employee-service.interface';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-employee-addedit',
  imports: [FontAwesomeModule, CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './employee-addedit.component.html',
  styleUrl: './employee-addedit.component.css',
})
export class EmployeeAddeditComponent implements OnInit {
  //#region Font Awesome Icons
  public faCamera: IconDefinition = faCamera;
  //#endregion
  public form!: FormGroup;
  public errorMessage: string = '';
  public showErrorMessage: boolean = false;

  @Input() public selectedItem!: EmployeeModel | undefined | null;
  @Output() close = new EventEmitter<void>();
  @Output() dataChanged = new EventEmitter<void>();

  onClose() {
    this.close.emit();
  }

  employee: any;

  constructor(
    @Inject(EMPLOYEE_SERVICE) private readonly employeeService: IEmployeeService
  ) {}

  ngOnInit(): void {
    this.createForm();
    this.updateForm();
  }

  public createForm() {
    this.form = new FormGroup({
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
      username: new FormControl({ value: '', disabled: !!this.selectedItem }, [
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
      identityCard: new FormControl('', [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(18),
        Validators.pattern('^[0-9]{10,18}$'),
      ]),
      phoneNumber: new FormControl(null, [
        Validators.pattern('/^[0-9]{10,15}$/'),
      ]),
      address: new FormControl(''),
    });
    this.form.valueChanges.subscribe(() => {
      this.showErrorMessage = false;
    });
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
  private updateForm(): void {
    this.form.patchValue(this.selectedItem as any);
  }

  public onSubmit(): void {
    if (this.form.invalid) {
      this.showErrorMessage = true;
      this.errorMessage = 'All fields marked with an asterisk are required';
      return;
    }

    const data: EmployeeModel = this.form.value;

    // Call API
    if (this.selectedItem) {
      // Update
      // Assign id to data
      Object.assign(data, { id: this.selectedItem.id });

      this.employeeService
        .update(this.selectedItem.id, data)
        .subscribe((res) => {
          if (res) {
            console.log('Update success');
            // Search data again
            // Close detail
            this.onClose();
          } else {
            console.log('Update failed');
          }
        });
    } else {
      // Create
      this.employeeService.create(data).subscribe((res) => {
        if (res) {
          console.log('Create success');
          // Search data again
          // Close detail
          this.onClose();
        } else {
          console.log('Create failed');
        }
      });
    }
  }
}
