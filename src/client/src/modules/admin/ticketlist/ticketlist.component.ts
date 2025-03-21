import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faInfoCircle, faAngleRight, faAngleLeft, faFilter, faRotateLeft, faAnglesRight, faAnglesLeft } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-ticketlist',
  imports: [FontAwesomeModule, CommonModule, RouterModule, FormsModule],
  templateUrl: './ticketlist.component.html',
  styleUrl: './ticketlist.component.css'
})
export class TicketlistComponent {
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
  public currentPage: number = 1;
  public totalPages: number = 10;

  public toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

}
