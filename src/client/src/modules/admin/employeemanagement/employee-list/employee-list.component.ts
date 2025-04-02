import { Component, Inject, OnInit } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faArrowLeft,
  faEdit,
  faTrash,
  faInfoCircle,
  faAngleRight,
  faAngleLeft,
  faFilter,
  faAnglesLeft,
  faAnglesRight,
  faRotateLeft,
} from '@fortawesome/free-solid-svg-icons';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { EmployeeAddeditComponent } from '../employee-addedit/employee-addedit.component';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MasterDataListComponent } from '../../../../core/components/master-data/master-data.component';
import { UserModel } from '../../../../models/user/user.model';
import { EMPLOYEE_SERVICE } from '../../../../constants/injection.constant';
import { IEmployeeService } from '../../../../services/employee/employee-service.interface';
import { TableColumn } from '../../../../core/models/table-column.model';
import { TableComponent } from '../../../../core/components/table/table.component';
import { ServicesModule } from '../../../../services/services.module';

@Component({
  selector: 'app-employeemanagement',
  imports: [
    FontAwesomeModule,
    EmployeeDetailComponent,
    EmployeeAddeditComponent,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TableComponent,
    ServicesModule,
  ],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
})
export class EmployeemanagementComponent
  extends MasterDataListComponent<UserModel>
  implements OnInit
{
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faFilter: IconDefinition = faFilter;
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  currentView: 'list' | 'detail' | 'add' | 'edit' = 'list';

  public override columns: TableColumn[] = [
    { name: 'Username', value: 'username' },
    { name: 'Full Name', value: 'displayName' },
    { name: 'Gender', value: 'gender' },
    { name: 'Date of birth', value: 'dateOfBirth', type: 'date' },
    { name: 'Email', value: 'email' },
    { name: 'Phone Number', value: 'phoneNumber' },
  ];
  constructor(
    @Inject(EMPLOYEE_SERVICE) private readonly employeeService: IEmployeeService
  ) {
    super();
  }
  protected override createForm(): void {
    this.searchForm = new FormGroup(
      {
        keyword: new FormControl(''),
        gender: new FormControl(''),
        birthDateStart: new FormControl(null),
        birthDateEnd: new FormControl(null),
      },
      { validators: [this.crossFieldValidator] }
    );
  }
  syncDateValidation(changedField: 'start' | 'end') {
    const startCtrl = this.searchForm.get('birthDateStart');
    const endCtrl = this.searchForm.get('birthDateEnd');

    if (changedField === 'start') {
      // Khi birthDateStart thay đổi
      if (startCtrl?.value && endCtrl?.value) {
        this.validateDateOrder(startCtrl, endCtrl);
      }
      endCtrl?.updateValueAndValidity(); // Cập nhật validation birthDateEnd
    } else {
      // Khi birthDateEnd thay đổi
      if (startCtrl?.value && endCtrl?.value) {
        this.validateDateOrder(startCtrl, endCtrl);
      }
      startCtrl?.updateValueAndValidity(); // Cập nhật validation birthDateStart
    }
  }

  // Kiểm tra thứ tự ngày
  private validateDateOrder(
    startCtrl: AbstractControl,
    endCtrl: AbstractControl
  ) {
    const birthDateStart = new Date(startCtrl.value);
    const birthDateEnd = new Date(endCtrl.value);

    if (birthDateStart > birthDateEnd) {
      startCtrl.setErrors({ invalidRange: true });
      endCtrl.setErrors({ invalidRange: true });
    } else {
      startCtrl.setErrors(null);
      endCtrl.setErrors(null);
    }
  }

  // Cross-field validator
  crossFieldValidator(control: AbstractControl) {
    const start = control.get('birthDateStart')?.value;
    const end = control.get('birthDateEnd')?.value;

    if (!start || !end) return null;

    return new Date(start) <= new Date(end) ? null : { dateOrderInvalid: true };
  }

  protected override searchData(): void {
    this.employeeService.search(this.filter).subscribe((res) => {
      this.data = res;
    });
  }

  public delete(id: string): void {
    this.employeeService.delete(id).subscribe((data) => {
      // Neu xoa duoc thi goi lai ham getData de load lai du lieu
      if (data) {
        this.searchData();
      }
    });
  }
  public isDropdownOpen: boolean = false;
  // public currentPage: number = 1;
  // public totalPages: number = 10;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  // onViewDetail(employee: any): void {
  //   this.employeeManagementService.goToDetail(employee);
  // }

  // onAdd(): void {
  //   this.employeeManagementService.goToAdd();
  // }

  // onEdit(employee: any): void {
  //   this.employeeManagementService.goToEdit(employee);
  // }
  // onDelete(employee: any): void {
  //   this.employeeManagementService.goToList();
  // }
}
