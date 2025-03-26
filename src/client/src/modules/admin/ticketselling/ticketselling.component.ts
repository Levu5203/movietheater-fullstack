import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faAngleLeft,
  faAngleRight,
  faAnglesLeft,
  faAnglesRight,
  faArrowLeft,
} from '@fortawesome/free-solid-svg-icons';
import { TiketsellingSelectseatComponent } from "./tiketselling-selectseat/tiketselling-selectseat.component";
import { TicketsellingPaymentComponent } from "./ticketselling-payment/ticketselling-payment.component";
import { TicketSellingService } from './ticketselling.service';

@Component({
  selector: 'app-ticketselling',
  imports: [FontAwesomeModule, CommonModule, FormsModule, TiketsellingSelectseatComponent, TicketsellingPaymentComponent],
  templateUrl: './ticketselling.component.html',
  styleUrls: ['./ticketselling.component.css'],
  standalone: true,
})
export class TicketsellingComponent implements OnInit {
  //#region Font Awesome Icons
  public faArrowLeft: IconDefinition = faArrowLeft;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAnglesRight: IconDefinition = faAnglesRight;
  //#endregion

  public currentPage: number = 1;
  public totalPages: number = 10;

  currentView: 'ticketselling' | 'select-seat' | 'payment' = 'ticketselling';

  constructor(private ticketService: TicketSellingService) {}

  ngOnInit() {
    this.ticketService.getCurrentView().subscribe(view => {
      this.currentView = view;
    });
  }
  onSelectShowtime() {
    this.ticketService.setCurrentView('select-seat');
  }
}
