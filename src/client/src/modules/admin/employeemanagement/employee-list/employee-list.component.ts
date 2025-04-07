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
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MasterDataListComponent } from '../../../../core/components/master-data/master-data.component';
import { EMPLOYEE_SERVICE } from '../../../../constants/injection.constant';
import { IEmployeeService } from '../../../../services/employee/employee-service.interface';
import { TableColumn } from '../../../../core/models/table-column.model';
import { TableComponent } from '../../../../core/components/table/table.component';
import { ServicesModule } from '../../../../services/services.module';
import { EmployeeModel } from '../../../../models/user/employee.model';

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
  extends MasterDataListComponent<EmployeeModel>
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

  public override columns: TableColumn[] = [
    { name: 'Username', value: 'username' },
    { name: 'Full Name', value: 'displayName' },
    { name: 'Year of birth', value: 'dateOfBirth', type: 'year' },
    { name: 'Gender', value: 'gender' },
    { name: 'Email', value: 'email' },
    { name: 'Phone Number', value: 'phoneNumber' },
    { name: 'Status', value: 'isActive', type: 'active' },
    { name: 'Register date', value: 'createdAt', type: 'date' },
  ];
  constructor(
    @Inject(EMPLOYEE_SERVICE) private readonly employeeService: IEmployeeService
  ) {
    super();
  }
  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
      gender: new FormControl(''),
      isActive: new FormControl(null),
    });
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
  public edit(id: string): void {
    this.isShowDetail = false;
    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowForm = true;

      // Scroll into view
    }, 150);
  }

  public create(): void {
    this.isShowDetail = false;
    setTimeout(() => {
      this.selectedItem = null;
      this.isShowForm = true;

      // Scroll into view
    }, 150);
  }

  public view(id: string): void {
    this.isShowForm = false;
    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowDetail = true;

      // Scroll into view
    }, 150);
  }

  public updateStatus(id: string): void {
    this.employeeService.updateStatus(id).subscribe((data) => {
      // Neu xoa duoc thi goi lai ham getData de load lai du lieu
      if (data) {
        this.searchData();
      }
    });
  }
}
