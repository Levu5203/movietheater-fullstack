import { Component, Inject, OnInit } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
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
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserModel } from '../../../models/user/user.model';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { TableColumn } from '../../../core/models/table-column.model';
import { CUSTOMER_SERVICE } from '../../../constants/injection.constant';
import { ICustomerService } from '../../../services/customer/customer-service.interface';
import { TableComponent } from '../../../core/components/table/table.component';
import { ServicesModule } from '../../../services/services.module';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';

@Component({
  selector: 'app-customermanagement',
  imports: [
    FontAwesomeModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ServicesModule,
    TableComponent,
    CustomerDetailComponent,
  ],
  templateUrl: './customermanagement.component.html',
  styleUrl: './customermanagement.component.css',
})
export class CustomermanagementComponent
  extends MasterDataListComponent<UserModel>
  implements OnInit
{
  //#region Font Awesome Icons
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
    { name: 'Gender', value: 'gender' },
    { name: 'Year of birth', value: 'dateOfBirth', type: 'year' },
    { name: 'Email', value: 'email' },
    { name: 'Phone Number', value: 'phoneNumber' },
  ];

  constructor(
    @Inject(CUSTOMER_SERVICE) private readonly customerService: ICustomerService
  ) {
    super();
  }

  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
      gender: new FormControl(''),
    });
  }

  protected override searchData(): void {
    this.customerService.search(this.filter).subscribe((res) => {
      this.data = res;
    });
  }

  public delete(id: string): void {
    this.customerService.delete(id).subscribe((data) => {
      // Neu xoa duoc thi goi lai ham getData de load lai du lieu
      if (data) {
        this.searchData();
      }
    });
  }
  public view(id: string): void {
    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowDetail = true;

      // Scroll into view
    }, 150);
  }
}
