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
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserModel } from '../../../models/user/user.model';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { TableColumn } from '../../../core/models/table-column.model';
import { USER_SERVICE } from '../../../constants/injection.constant';
import { IUserService } from '../../../services/user/user-service.interface';
import { TableComponent } from '../../../core/components/table/table.component';
import { ServicesModule } from '../../../services/services.module';

@Component({
  selector: 'app-customermanagement',
  imports: [
    FontAwesomeModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ServicesModule,
    TableComponent,
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

  public isDropdownOpen: boolean = false;
  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  public override columns: TableColumn[] = [
    { name: 'Username', value: 'username' },
    { name: 'Full Name', value: 'displayName' },
    { name: 'Gender', value: 'gender' },
    { name: 'Date of birth', value: 'dateOfBirth' },
    { name: 'Email', value: 'email' },
    { name: 'Phone Number', value: 'phoneNumber' },
  ];

  constructor(@Inject(USER_SERVICE) private readonly userService: IUserService) {
    super();
  }

  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
      gender: new FormControl(''),
      birthDateStart: new FormControl(null),
      birthDateEnd: new FormControl(null),
    });
  }

  protected override searchData(): void {
    this.userService.search(this.filter).subscribe((res) => {
      this.data = res;
    });
  }

  public delete(id: string): void {
    this.userService.delete(id).subscribe((data) => {
      // Neu xoa duoc thi goi lai ham getData de load lai du lieu
      if (data) {
        this.searchData();
      }
    });
  }
}
