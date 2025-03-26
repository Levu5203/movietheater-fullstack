import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketService } from '../ticket.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-ticketselling-payment',
  imports: [FontAwesomeModule, CommonModule],
  templateUrl: './ticketselling-payment.component.html',
  styleUrls: ['./ticketselling-payment.component.css'],
  standalone: true,
})
export class TicketsellingPaymentComponent implements OnInit {

  selectedSeats: any[] = [];
  totalPrice: number = 0;

  isShowMemberInfo: boolean = false;

  constructor(private ticketService: TicketService) {}

  checkMember() {
    this.isShowMemberInfo = true;
  }

  ngOnInit() {
    this.ticketService.getSelectedSeats().subscribe((seats) => {
      this.selectedSeats = seats;
    });

    this.ticketService.getTotalPrice().subscribe((price) => {
      this.totalPrice = price;
    });
  }

  onCancel() {
    this.ticketService.setCurrentView('select-seat');
  }

  onConfirmPayment() {
    // Xử lý thanh toán ở đây
    console.log('Thanh toán thành công!');
    this.ticketService.setCurrentView('ticketselling');
  }
}
