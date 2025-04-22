import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { InvoiceViewModel } from '../../../../models/invoice/invoiceview.model';
import { CommonModule} from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faUser } from '@fortawesome/free-solid-svg-icons';
import { IBookingService } from '../../../../services/booking/booking-service.interface';
import { BOOKING_SERVICE } from '../../../../constants/injection.constant';

@Component({
  selector: 'app-booking-detail',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './booking-detail.component.html',
  styleUrls: ['./booking-detail.component.css'],
})
export class BookingDetailComponent {
  @Input() public selectedItem!: InvoiceViewModel | undefined | null;
  @Output() close = new EventEmitter<void>();
  // FontAwesome icons
  faArrowLeft = faArrowLeft;
  faUser = faUser;

  constructor(
    @Inject(BOOKING_SERVICE) private readonly bookingService: IBookingService
  ) {}

  // Method to format dates
  formatDate(date: Date | undefined): string {
    if (!date) return 'N/A';
    const newDate = new Date(date);
    const day = newDate.getDate().toString().padStart(2, '0');
    const month = (newDate.getMonth() + 1).toString().padStart(2, '0');
    const year = newDate.getFullYear();
    return `${day}/${month}/${year}`;
  }

  // Method to format time (HH:mm)
  formatTime(time: Date | string | undefined | null): string {
    if (!time) return 'N/A';

    // Case 1: Already a Date object
    if (time instanceof Date) {
      if (isNaN(time.getTime())) return 'N/A';
      const hours = time.getHours().toString().padStart(2, '0');
      const minutes = time.getMinutes().toString().padStart(2, '0');
      return `${hours}:${minutes}`;
    }

    // Case 2: HH:mm:ss string
    const [hours, minutes] = time.split(':');
    if (!hours || !minutes) return 'N/A';

    return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}`;
  }

  onGetTicket(): void {
    if (this.selectedItem) {
      this.bookingService.update(this.selectedItem?.id,).subscribe((res) => {
        if (res) {
          alert('Get ticket successfully!');
          this.onClose();
        } else {
          alert('Cannot get ticket!');
        }
      });;
    }
  }

  // Close method (could trigger an event to close the detail view)
  onClose(): void {
    this.close.emit();
  }
}
