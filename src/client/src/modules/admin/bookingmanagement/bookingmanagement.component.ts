import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faArrowLeft,
  faInfoCircle,
  faAngleRight,
  faAngleLeft,
  faFilter,
  faRotateLeft,
  faAnglesRight,
  faAnglesLeft,
} from '@fortawesome/free-solid-svg-icons';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MasterDataListComponent } from '../../../core/components/master-data/master-data.component';
import { InvoiceViewModel } from '../../../models/invoice/invoiceview.model';
import { TableColumn } from '../../../core/models/table-column.model';
import { IBookingService } from '../../../services/booking/booking-service.interface';
import { BOOKING_SERVICE } from '../../../constants/injection.constant';
import { TableComponent } from '../../../core/components/table/table.component';
import { ServicesModule } from '../../../services/services.module';
import { BookingDetailComponent } from './booking-detail/booking-detail.component';

@Component({
  selector: 'app-ticketlist',
  imports: [
    FontAwesomeModule,
    CommonModule,
    RouterModule,
    FormsModule,
    ServicesModule,
    ReactiveFormsModule,
    TableComponent,
    BookingDetailComponent,
  ],
  templateUrl: './bookingmanagement.component.html',
  styleUrl: './bookingmanagement.component.css',
})
export class BookingManagementComponent
  extends MasterDataListComponent<InvoiceViewModel>
  implements OnInit
{
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faFilter: IconDefinition = faFilter;
  public faRotateLeft: IconDefinition = faRotateLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  //#endregion

  public isDropdownOpen: boolean = false;
  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  public override columns: TableColumn[] = [
    { name: 'Full Name', value: 'userFullName' },
    { name: 'Email', value: 'userEmail' },
    { name: 'Phone Number', value: 'userPhoneNumber' },
    { name: 'Room', value: 'roomName' },
    { name: 'Movie', value: 'movieName' },
    { name: 'Show Date', value: 'showDate  ' },
    { name: 'Time', value: 'startTime' },
    { name: 'Seat', value: 'seats' },
    { name: 'Status', value: 'ticketIssued' },
  ];

  constructor(
    @Inject(BOOKING_SERVICE) private readonly bookingService: IBookingService
  ) {
    super();
  }

  protected override createForm(): void {
    this.searchForm = new FormGroup({
      keyword: new FormControl(''),
      ticketIssued: new FormControl(null),
    });
  }

  protected override searchData(): void {
    this.bookingService.search(this.filter).subscribe((res) => {
      this.data = res;
    });
  }

  public view(id: string): void {
    console.log('on view');

    setTimeout(() => {
      this.selectedItem = this.data.items.find((x) => x.id === id);
      this.isShowDetail = true;

      // Scroll into view
    }, 150);
  }
}
