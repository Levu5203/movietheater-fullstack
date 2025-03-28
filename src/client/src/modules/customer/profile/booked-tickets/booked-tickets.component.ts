import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faCalendar,
  faSearch,
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
@Component({
  selector: 'app-booked-tickets',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './booked-tickets.component.html',
  styleUrl: './booked-tickets.component.css',
})
export class BookedTicketsComponent {
  public faSearch: IconDefinition = faSearch;
  public faCalendar: IconDefinition = faCalendar;
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

  public isDropdownOpen: boolean = false;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
