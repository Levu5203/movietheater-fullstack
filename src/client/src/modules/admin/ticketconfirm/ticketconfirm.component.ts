import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-ticketconfirm',
  imports: [],
  templateUrl: './ticketconfirm.component.html',
  styleUrl: './ticketconfirm.component.css'
})
export class TicketconfirmComponent {
  constructor(private location: Location) {}

  goBack() {
    this.location.back(); // Quay về trang trước đó
  }
}
